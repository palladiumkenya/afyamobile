using System;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model.Config;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class PullDataViewModel:MvxViewModel, IPullDataViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly ISettings _settings;
        private readonly IActivationService _activationService;
        private readonly IMetaSyncService _metaSyncService;
        private readonly IFormsSyncService _formsSyncService;
        private readonly IDeviceSetupService _deviceSetupService;
        private readonly IDataSyncService _dataSyncService;
        private readonly ISyncDataService _syncDataService;
        private string _address;
        private string _currentStatus;
        private int _currentStatusProgress;
        private string _overallStatus;
        private int _overallStatusProgress;
        private IMvxCommand _pullDataCommand;
        private bool _isBusy;
        

        public Device Device { get; set; }
        public ServerConfig Local { get; set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy);}
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; RaisePropertyChanged(() => Address); }
        }

        public string CurrentStatus
        {
            get { return _currentStatus; }
            set { _currentStatus = value; RaisePropertyChanged(() => CurrentStatus); }
        }

        public int CurrentStatusProgress
        {
            get { return _currentStatusProgress; }
            set { _currentStatusProgress = value; RaisePropertyChanged(() => CurrentStatusProgress); }
        }

        public string OverallStatus
        {
            get { return _overallStatus; }
            set { _overallStatus = value; RaisePropertyChanged(() => OverallStatus); }
        }

        public int OverallStatusProgress
        {
            get { return _overallStatusProgress; }
            set { _overallStatusProgress = value; RaisePropertyChanged(() => OverallStatusProgress); }
        }

        public IMvxCommand PullDataCommand
        {
            get
            {
                _pullDataCommand = _pullDataCommand ?? new MvxCommand(PullData, CanPullData);
                return _pullDataCommand;
            }
        }
        public PullDataViewModel(IDialogService dialogService, ISettings settings, IDeviceSetupService deviceSetupService, IActivationService activationService, IMetaSyncService metaSyncService, ISyncDataService syncDataService, IFormsSyncService formsSyncService, IDataSyncService dataSyncService)
        {
            _dialogService = dialogService;
            _settings = settings;
            _deviceSetupService = deviceSetupService;
            _activationService = activationService;
            _metaSyncService = metaSyncService;
            _syncDataService = syncDataService;
            _formsSyncService = formsSyncService;
            _dataSyncService = dataSyncService;
        }

        public void Init()
        {
            IsBusy = false;
            LoadInit();
        }

        public override void ViewAppeared()
        {
            IsBusy = false;
            LoadInit();
        }
        private void LoadInit()
        {
            var deviceJson = _settings.GetValue("device.id", "");
            var hapiLocal = _settings.GetValue("hapi.local", "");

            if (null == Device && !string.IsNullOrWhiteSpace(deviceJson))
            {
                Device = JsonConvert.DeserializeObject<Device>(deviceJson);
            }
            else
            {
                Device = _deviceSetupService.GetDefault();
                if (null == Device)
                {
                    Device = new Device();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(Device);
                    _settings.AddOrUpdateValue("device.id", json);
                }
            }

            if (null == Local && !string.IsNullOrWhiteSpace(hapiLocal))
            {
                Local = JsonConvert.DeserializeObject<ServerConfig>(hapiLocal);
            }
            else
            {
                Local = _deviceSetupService.GetLocal();
                if (null == Local)
                {
                    Local = new ServerConfig();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(Local);
                    _settings.AddOrUpdateValue("hapi.local", json);
                }
            }
            Address = Local.Address;
        }

        private bool CanPullData()
        {
            return !string.IsNullOrEmpty(Address);
        }

        private async void PullData()
        {
            int total = 11;
            int current = 0;
            IsBusy = true;
            CurrentStatus = $"connecting...";
            var practice = await _activationService.GetLocal(Address);
            if (null != practice)
            {
                current++;
                CurrentStatus = showPerc("Metas",current, total);
                var meta = await _metaSyncService.GetMetaData(Address); 
                _syncDataService.UpdateMeta(meta);

                current++;
                CurrentStatus = showPerc("Counties", current, total);
                var counties = await  _metaSyncService.GetCounties(Address);
                _syncDataService.Update(counties);

                current++;
                CurrentStatus = showPerc("Categories", current, total);
                var categories = await _metaSyncService.GetCategories(Address);
                _syncDataService.Update(categories);

                current++;
                CurrentStatus = showPerc("Items", current, total);
                var items = await _metaSyncService.GetItems(Address);
                _syncDataService.Update(items);

                current++;
                CurrentStatus = showPerc("Modules", current, total);
                var modules = await _formsSyncService.GetModules(Address);
                _syncDataService.UpdateModules(modules);

                current++;
                CurrentStatus = showPerc("Forms", current, total);
                var forms = await _formsSyncService.GetForms(Address);
                _syncDataService.UpdateForms(forms);

                current++;
                CurrentStatus = showPerc("Concepts", current, total);
                var concepts = await _formsSyncService.GetConcepts(Address);
                _syncDataService.UpdateConcepts(concepts);

                current++;
                CurrentStatus = showPerc("Staff", current, total);
                var persons = await _dataSyncService.GetStaff(Address);
                _syncDataService.UpdateStaff(persons);


                current++;
                CurrentStatus = showPerc("Staff", current, total);
                var persons = await _dataSyncService.GetUsers(Address);
                _syncDataService.UpdateStaff(persons);


                current++;
                CurrentStatus = showPerc("Staff", current, total);
                var persons = await _dataSyncService.GetProviders(Address);
                _syncDataService.UpdateStaff(persons);


                current++;
                CurrentStatus = showPerc("Staff", current, total);
                var persons = await _dataSyncService.GetStaff(Address);
                _syncDataService.UpdateStaff(persons);





                CurrentStatus = "done!";
                _dialogService.ShowToast("completed successfully");
            }
            else
            {
                IsBusy = false;
                CurrentStatus = $"connection failed !!";

                _dialogService.Alert("Could not connect to server");
            }

            IsBusy = false;
            
        }

   

        private string showPerc(string message, int complete,int total)
        {
            int percentComplete = (int)Math.Round((double)(100 * complete) / total);

            return $"downloading {message}...  {percentComplete}% ";
        }
    }
}