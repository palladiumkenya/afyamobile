using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Service.Config
{
    public class DeviceSetupService:IDeviceSetupService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IServerConfigRepository _serverConfigRepository;
        private readonly IPracticeRepository _practiceRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IModuleRepository _moduleRepository;

        public DeviceSetupService(IDeviceRepository deviceRepository, IServerConfigRepository serverConfigRepository, IPracticeRepository practiceRepository, IPersonRepository personRepository, IProviderRepository providerRepository, IUserRepository userRepository, IModuleRepository moduleRepository)
        {
            _deviceRepository = deviceRepository;
            _serverConfigRepository = serverConfigRepository;
            _practiceRepository = practiceRepository;
            _personRepository = personRepository;
            _providerRepository = providerRepository;
            _userRepository = userRepository;
            _moduleRepository = moduleRepository;
        }

        public bool IsSetup()
        {
            var local = GetLocal();
            return null != local && local.IsSetupComplete();
        }

        public bool HasPulledData()
        {
          
            return _moduleRepository.Count()>0;
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

        public void UpdateCode(string prefix)
        {
            var currentDevices = _deviceRepository.GetAll().ToList();

            foreach (var d in currentDevices)
            {
                d.Code = prefix;
                _deviceRepository.Update(d);
            }
        }

        public void SavePractce(Practice practice)
        {
            var practices = _practiceRepository.GetAll().ToList();

            if (practices.Count == 0)
            {
                _practiceRepository.Save(practice);

            }

            foreach (var p in practices)
            {
                p.IsDefault = false;
                _practiceRepository.Update(p);
            }
            practice.IsDefault = true;
            _practiceRepository.InsertOrUpdate(practice);
        }

        public void SaveUsers(List<User> users)
        {
            foreach (var user in users)
            {
                _personRepository.InsertOrUpdate(user.Person);
                _userRepository.InsertOrUpdate(user);
                _providerRepository.InsertOrUpdate(user.Provider);
            }
        }
    }
}