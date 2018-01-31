using System;
using System.Text;
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
    public class SetupWizardViewModel:MvxViewModel, ISetupWizardViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IEmrService _emrService;
        private readonly ISetupWizardService _setupWizardService;
        private readonly IDeviceSetupService _deviceSetupService;
        private readonly ISettings _settings;

        private string _emr;
        private string _url;
        private string _facility;
        private string _status;
        private bool _loading;
        private IMvxCommand _setupDeviceCommand;
        private string _serial;
        private string _name;
        private ServerConfig _local;
        private string _setupAction;


        public Device Device { get; set; }

        public ServerConfig Local
        {
            get { return _local; }
            set
            {
                _local = value; RaisePropertyChanged(() => Local);
                if (null != Local)
                {
                    Facility = Local.Name;
                    Status = Local.IsSetupComplete()
                        ? "Device is already setup "
                        : "Not Setup";
                    //Status =  Local.
                    SetupAction = Local.IsSetupComplete()
                        ? "Setup Again"
                        : "Setup";

                }
            }
        }

        public string Serial
        {
            get { return _serial; }
            set { _serial = value; RaisePropertyChanged(() => Serial); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        public string Emr
        {
            get { return _emr; }
            set { _emr = value; RaisePropertyChanged(() => Emr); SetupDeviceCommand.RaiseCanExecuteChanged(); }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; RaisePropertyChanged(() => Url); SetupDeviceCommand.RaiseCanExecuteChanged(); }
        }

        public string Facility
        {
            get { return _facility; }
            set { _facility = value; RaisePropertyChanged(() => Facility); }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; RaisePropertyChanged(() => Status); }
        }

        public bool Loading
        {
            get { return _loading; }
            set { _loading = value; RaisePropertyChanged(() => Loading); }
        }

        public string SetupAction
        {
            get { return _setupAction; }
            set { _setupAction = value; RaisePropertyChanged(() => SetupAction); }
        }

        public IMvxCommand SetupDeviceCommand
        {
            get
            {
                _setupDeviceCommand = _setupDeviceCommand ?? new  MvxCommand(SetupDevice, CanSetupDevice);
                return _setupDeviceCommand;
            }
        }


        public SetupWizardViewModel(IDialogService dialogService, ISetupWizardService setupWizardService, IDeviceSetupService deviceSetupService, ISettings settings, IEmrService emrService)
        {
            _dialogService = dialogService;
            _setupWizardService = setupWizardService;
            _deviceSetupService = deviceSetupService;
            _settings = settings;
            _emrService = emrService;
            Emr = "IQCare";
            SetupAction = "Setup";
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
            var deviceJson = _settings.GetValue("device.id", "");
            var hapiLocal = _settings.GetValue("hapi.local", "");

            if (null == Device && !string.IsNullOrWhiteSpace(deviceJson))
            {
                Device = JsonConvert.DeserializeObject<Device>(deviceJson);
            }
            else
            {
                Device = _deviceSetupService.GetDefault(Serial);
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
            Url = Local.Address;
       }

       


        public void LoadDeviceInfo(string serial, string name, string manufacturer)
        {
            Serial = serial;
            Name = $"{manufacturer},{name}";
        }

        private bool CanSetupDevice()
        {
            return !string.IsNullOrWhiteSpace(Emr) && !string.IsNullOrWhiteSpace(Url);
        }

        private async void SetupDevice()
        {
            Loading = true;
            _dialogService.ShowWait("Verifying,Please wait...");

            //save device info
            Device.Serial = Serial;
            Device.Name = Name;

            _deviceSetupService.Register(Device);
            Device = _deviceSetupService.GetDefault(Device.Id);
            if (null != Device)
                _settings.AddOrUpdateValue("device.id", JsonConvert.SerializeObject(Device));

            //get fac
            var practice = await _emrService.GetDefault(Url);
            if (null != practice)
            {
                Device.PracticeId = practice.Id;
                Local = ServerConfig.CreateLocal(practice, Url, true);

                //update dev
                _deviceSetupService.Register(Device);

                
                _deviceSetupService.SaveLocal(Local);

                //save fac
                _deviceSetupService.SavePractce(practice);

                Local = _deviceSetupService.GetLocal();
            }
            else
            {
                _dialogService.Alert("Address could not be verified");
                _dialogService.HideWait();
                return;
            }

            //get users
            var users = await _emrService.GetUsers(Url);
            _deviceSetupService.SaveUsers(users);


            Loading = false;
            _dialogService.HideWait();

            
            if (Local.IsSetupComplete())
            {
                _dialogService.ShowToast("Device setup successfully");
                ShowViewModel<SignInViewModel>();
            }
            else
            {
                _dialogService.Alert("Please setup device before proceeding");
            }
                

        }

        public SetupWizardViewModel(IEmrService emrService)
        {
            _emrService = emrService;
            _emr = "IQCare";
        }
    }
}