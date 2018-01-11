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
        private readonly IServerConfigRepository _serverConfigRepository;
        private readonly IPracticeRepository _practiceRepository;

        public DeviceSetupService(IDeviceRepository deviceRepository, IServerConfigRepository serverConfigRepository, IPracticeRepository practiceRepository)
        {
            _deviceRepository = deviceRepository;
            _serverConfigRepository = serverConfigRepository;
            _practiceRepository = practiceRepository;
        }

        public Device GetDefault(Guid deviceId)
        {
            return _deviceRepository.Get(deviceId);
        }

        public Device GetDefault(string serial)
        {
            return _deviceRepository.GetDefault(serial);
        }

        public ServerConfig GetCentral()
        {
            return _serverConfigRepository.Get("hapi.central");
        }

        public ServerConfig GetLocal()
        {
            return _serverConfigRepository.Get("hapi.local");
        }

        public void SaveCentral(ServerConfig config)
        {
            _serverConfigRepository.InsertOrUpdate(config);
        }

        public void SaveLocal(ServerConfig config)
        {
            _serverConfigRepository.InsertOrUpdate(config);
        }

        public void Register(Device device)
        {
            var currentDevices = _deviceRepository.GetAll().ToList();

            foreach (var d in currentDevices)
            {
                d.IsDefault = false;
                _deviceRepository.Update(d);
            }
            device.IsDefault = true;
            _deviceRepository.InsertOrUpdate(device);
        }

        public void CheckRegister(Device device)
        {
            var ddevice = _deviceRepository.GetDefault(device.Serial);
            if(null== ddevice)
                Register(device);
        }
    }
}