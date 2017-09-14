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
    public class PracticeViewModel:MvxViewModel,IPracticeViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly ISettings _settings;
        private readonly IPracticeSetupService _practiceSetupService;
        private readonly IActivationService _activationService;
        private readonly IDeviceSetupService _deviceSetupService;

        private string _code;
        private string _name;
        private string _practiceTypeId;
        private int? _countyId;
        private IMvxCommand _searchPracticeCommand;
        private IMvxCommand _savePracticeCommand;

        public Practice Practice { get; set; }
        public ServerConfig Central { get; set; }
        public ServerConfig Local { get; set; }

        public string Code
        {
            get { return _code; }
            set { _code = value; RaisePropertyChanged(() => Code);SavePracticeCommand.RaiseCanExecuteChanged();SearchPracticeCommand.RaiseCanExecuteChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); SavePracticeCommand.RaiseCanExecuteChanged(); SearchPracticeCommand.RaiseCanExecuteChanged(); }
        }

        public string PracticeTypeId
        {
            get { return _practiceTypeId; }
            set { _practiceTypeId = value; RaisePropertyChanged(() => PracticeTypeId); }
        }

        public int? CountyId
        {
            get { return _countyId; }
            set { _countyId = value; RaisePropertyChanged(() => CountyId); }
        }

        public IMvxCommand SearchPracticeCommand
        {
            get
            {
                _searchPracticeCommand = _searchPracticeCommand ?? new MvxCommand(SearchPractice, CanSearchPractice);
                return _searchPracticeCommand;
            }
        }

        public IMvxCommand SavePracticeCommand
        {
            get
            {
                _savePracticeCommand = _savePracticeCommand ?? new MvxCommand(SavePractice, CanSavePractice);
                return _savePracticeCommand;
            }
        }

        public PracticeViewModel(IDialogService dialogService, ISettings settings, IPracticeSetupService practiceSetupService, IActivationService activationService, IDeviceSetupService deviceSetupService)
        {
            _dialogService = dialogService;
            _settings = settings;
            _practiceSetupService = practiceSetupService;
            _activationService = activationService;
            _deviceSetupService = deviceSetupService;
        }
        public void Init()
        {
            LoadInit();
        }

        public override void ViewAppeared()
        {
            LoadInit();
        }

        private void LoadInit()
        {
            var deviceJson = _settings.GetValue("device.practice", "");
            var hapiCentral = _settings.GetValue("hapi.central", "");
            var hapiLocal = _settings.GetValue("hapi.local", "");

            if (null == Practice && !string.IsNullOrWhiteSpace(deviceJson))
            {
                Practice = JsonConvert.DeserializeObject<Practice>(deviceJson);
            }
            else
            {
                Practice = _practiceSetupService.GetDefault();
                if (null == Practice)
                {
                    Practice = new Practice();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(Practice);
                    _settings.AddOrUpdateValue("device.practice", json);
                }
            }

            Code = Practice.Code;
            Name = Practice.Name;

            if (null == Central && !string.IsNullOrWhiteSpace(hapiCentral))
            {
                Central = JsonConvert.DeserializeObject<ServerConfig>(hapiCentral);
            }
            else
            {
                Central = _deviceSetupService.GetCentral();
                if (null == Central)
                {
                    Central = new ServerConfig();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(Central);
                    _settings.AddOrUpdateValue("hapi.central", json);
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
        }

        private bool CanSavePractice()
        {
            return !string.IsNullOrWhiteSpace(Code) && !string.IsNullOrWhiteSpace(Name);
        }

        private void SavePractice()
        {
            _practiceSetupService.Save(Practice);
            Practice = _practiceSetupService.GetDefault();
            _settings.AddOrUpdateValue("hapi.practice", JsonConvert.SerializeObject(Practice));
            _dialogService.ShowToast($"Saved {Practice} Successfully");
        }
        private bool CanSearchPractice()
        {
            return !string.IsNullOrWhiteSpace(Code) && Code.Trim().Length > 4;
        }

        private async void SearchPractice()
        {
            _dialogService.ShowWait("Searching...");

            var practice = await _activationService.SearchLocal(Local.Address, Code);

            if (null == practice)
            {
                practice = await _activationService.SearchCentral(Central.Address, Code);
            }
            _dialogService.HideWait();

            if (null == practice)
            {
                _dialogService.Alert("Could not Find facility");
            }
            else
            {
                Practice = practice;
                Name = practice.Name;
                _dialogService.ShowToast($"Facility Found {Practice}");

            }
        }
    }
}