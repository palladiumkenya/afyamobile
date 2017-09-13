using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Service.Config
{
    public class DeviceSetupService:IDeviceSetupService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceSetupService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public Device GetDefault(Guid deviceId)
        {
            return _deviceRepository.Get(deviceId);
        }

        public Device GetDefault(string serial)
        {
            return _deviceRepository.GetDefault(serial);
        }

        public void Register(Device device)
        {
            var currentDevices = _deviceRepository.GetAll().ToList();

            foreach (var d in currentDevices)
            {
                _deviceRepository.Delete(d.Id);
            }

            _deviceRepository.Save(device);
        }
    }
}