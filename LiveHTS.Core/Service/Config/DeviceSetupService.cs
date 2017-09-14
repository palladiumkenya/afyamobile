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
                _deviceRepository.Delete(d.Id);
            }

            _deviceRepository.Save(device);
        }
    }
}