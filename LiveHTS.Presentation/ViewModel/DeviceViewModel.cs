using System;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model.Config;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class DeviceViewModel:MvxViewModel, IDeviceViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IDeviceSetupService _deviceSetupService;
        private readonly IActivationService _activationService;
        private readonly ISettings _settings;

        private string _serial;
        private string _code;
        private string _name;
        private IMvxCommand _saveDeviceCommand;
        private string _centralAddress;
        private string _centralName;
        private string _localAddress;
        private string _localName;
        private IMvxCommand _verifyCentralCommand;
        private IMvxCommand _verifyLocalCommand;

        public Device Device { get; set; }

        public ServerConfig Central { get; set; }

        public ServerConfig Local { get; set; }

        public string Serial
        {
            get { return _serial; }
            set
            {
                _serial = value; RaisePropertyChanged(() => Serial);
                SaveDeviceCommand.RaiseCanExecuteChanged();
            }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value;  RaisePropertyChanged(() => Code);}
        }

        public string Name
        {
            get { return _name; }
            set { _name = value;  RaisePropertyChanged(() => Name);}
        }

        public string CentralAddress
        {
            get { return _centralAddress; }
            set { _centralAddress = value; RaisePropertyChanged(() => CentralAddress);VerifyCentralCommand.RaiseCanExecuteChanged(); }
        }

        public string CentralName
        {
            get { return _centralName; }
            set { _centralName = value; RaisePropertyChanged(() => CentralName); }
        }

        public string LocalAddress
        {
            get { return _localAddress; }
            set { _localAddress = value; RaisePropertyChanged(() => LocalAddress); VerifyLocalCommand.RaiseCanExecuteChanged();}
        }

        public string LocalName
        {
            get { return _localName; }
            set { _localName = value; RaisePropertyChanged(() => LocalName); }
        }

        public IMvxCommand SaveDeviceCommand
        {
            get
            {
                _saveDeviceCommand = _saveDeviceCommand ?? new MvxCommand(SaveDevice, CanSaveDevice);
                return _saveDeviceCommand;
            }
        }

        public IMvxCommand VerifyCentralCommand
        {
            get
            {
                _verifyCentralCommand = _verifyCentralCommand ?? new MvxCommand(VerifyCentral, CanVerifyCentral);
                return _verifyCentralCommand;
            }
        }

        public IMvxCommand VerifyLocalCommand
        {
            get
            {
                _verifyLocalCommand = _verifyLocalCommand ?? new MvxCommand(VerifyLocal, CanVerifyLocal);
                return _verifyLocalCommand;
            }
        }







        public DeviceViewModel(ISettings settings,IDialogService dialogService, IDeviceSetupService deviceSetupService, IActivationService activationService)
        {
            _dialogService = dialogService;
            _deviceSetupService = deviceSetupService;
            _activationService = activationService;
            _settings = settings;
        }


        public void Init()
        {
            LoadInit();
        }

        public override void ViewAppeared()
        {
           LoadInit();
        }
        public void LoadDeviceInfo(string serial, string name, string manufacturer)
        {
            Serial = serial;
            Name = $"{manufacturer},{name}";
        }
        private void LoadInit()
        {
            var deviceJson = _settings.GetValue("device.id", "");
            var hapiCentral = _settings.GetValue("hapi.central", "");
            var hapiLocal = _settings.GetValue("hapi.local", "");

            if (null== Device&&!string.IsNullOrWhiteSpace(deviceJson))
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
            Code = Device.Code;


            if (null== Central&&!string.IsNullOrWhiteSpace(hapiCentral))
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
            CentralAddress = Central.Address;
            CentralName = Central.Name;

            if (null==Local&&!string.IsNullOrWhiteSpace(hapiLocal))
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
            LocalAddress = Local.Address;
            LocalName = Local.Name;

        }

        private bool CanVerifyCentral()
        {
            return false;
            return !string.IsNullOrWhiteSpace(CentralAddress);
        }

        private async void VerifyCentral()
        {
            _dialogService.ShowWait("Verifying,Please wait...");
            Central = new ServerConfig("hapi.central");
            var practice = await _activationService.GetCentral(CentralAddress);
            _dialogService.HideWait();

            if (null != practice)
            {
                //Activate Device

                Central = ServerConfig.CreateCentral(practice, CentralAddress);
            }
            else
            {
                _dialogService.Alert("Address could not be verified");
            }
            CentralName = Central.Name;

        }

        private bool CanVerifyLocal()
        {
            return false;
            return !string.IsNullOrWhiteSpace(LocalAddress);
        }

        private async void VerifyLocal()
        {
            _dialogService.ShowWait("Verifying,Please wait...");
            Local = new ServerConfig("hapi.local");
            var practice = await _activationService.GetLocal(LocalAddress);
            _dialogService.HideWait();
            if (null != practice)
            {
                //Activate Device
                Device.PracticeId = practice.Id;
                Local = ServerConfig.CreateLocal(practice, LocalAddress);
            }
            else
            {
                _dialogService.Alert("Address could not be verified");
            }


            LocalName = Local.Name;

        }
        private bool CanSaveDevice()
        {
            return false;
            return !string.IsNullOrWhiteSpace(Serial);
        }

        private void SaveDevice()
        {
            Device.Serial = Serial;
            Device.Name = Name;
            Device.Code = Code;

            Central.Name = CentralName;
            Central.Address = CentralAddress;

            Local.Name = LocalName;
            Local.Address = LocalAddress;

            try
            {
                _deviceSetupService.Register(Device);
                _deviceSetupService.SaveCentral(Central);
                _deviceSetupService.SaveLocal(Local);

                Device = _deviceSetupService.GetDefault(Device.Id);
                if (null != Device)
                    _settings.AddOrUpdateValue("device.id", JsonConvert.SerializeObject(Device));

                Central = _deviceSetupService.GetCentral();
                if (null != Central)
                    _settings.AddOrUpdateValue("hapi.central", JsonConvert.SerializeObject(Central));

                Local = _deviceSetupService.GetLocal();
                if (null != Local)
                    _settings.AddOrUpdateValue("hapi.local", JsonConvert.SerializeObject(Local));


                _dialogService.ShowToast("Device info saved successfully");
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}
