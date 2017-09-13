using System;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Config;
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
        protected readonly ISettings _settings;

        private string _serial;
        private string _code;
        private string _name;
        private IMvxCommand _saveDeviceCommand;

        public Device Device { get; set; }

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

        public IMvxCommand SaveDeviceCommand
        {
            get
            {
                _saveDeviceCommand = _saveDeviceCommand ?? new MvxCommand(SaveDevice, CanSaveDevice);
                return _saveDeviceCommand;
            }
        }


        public void LoadDeviceInfo(string serial, string name,string manufacturer)
        {
            Serial = serial;
            Name = $"{manufacturer},{name}";
        }


        public DeviceViewModel(ISettings settings,IDialogService dialogService, IDeviceSetupService deviceSetupService)
        {
            _dialogService = dialogService;
            _deviceSetupService = deviceSetupService;
            _settings = settings;
        }

        public void Init()
        {
            var deviceJson = _settings.GetValue("device.id", "");
            if (!string.IsNullOrWhiteSpace(deviceJson))
            {
                Device = JsonConvert.DeserializeObject<Device>(deviceJson);
            }
            else
            {
                Device = _deviceSetupService.GetDefault(Serial);
                if (null == Device)
                    Device = new Device();
            }

            
            Code = Device.Code;
        }

        public override void ViewAppeared()
        {
            var deviceJson = _settings.GetValue("device.id", "");
            if (!string.IsNullOrWhiteSpace(deviceJson))
            {
                Device= JsonConvert.DeserializeObject<Device>(deviceJson);
            }
            else
            {
                Device = _deviceSetupService.GetDefault(Serial);
                if(null==Device)
                    Device=new Device();
            }
            
            Code = Device.Code;
        }
        private bool CanSaveDevice()
        {
            return !string.IsNullOrWhiteSpace(Serial);
        }

        private void SaveDevice()
        {
            Device.Serial = Serial;
            Device.Name = Name;
            Device.Code = Code;

            try
            {
                _deviceSetupService.Register(Device);
                Device = _deviceSetupService.GetDefault(Device.Id);
                _dialogService.ShowToast("Device info saved successfully");
            }
            catch (Exception e)
            {
                
                throw;
            }
            
        }
    }
}