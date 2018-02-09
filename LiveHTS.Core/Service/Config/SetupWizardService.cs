using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Service.Config
{
    public class SetupWizardService:ISetupWizardService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IServerConfigRepository _serverConfigRepository;
        private readonly IPracticeRepository _practiceRepository;

        public SetupWizardService(IDeviceRepository deviceRepository, IServerConfigRepository serverConfigRepository, IPracticeRepository practiceRepository)
        {
            _deviceRepository = deviceRepository;
            _serverConfigRepository = serverConfigRepository;
            _practiceRepository = practiceRepository;
        }


        public void UpdateDevice(Guid practiceId)
        {
            throw new NotImplementedException();
        }

        public void UpdateServerConfig(ServerConfig config)
        {
            throw new NotImplementedException();
        }

        public void UpdateUsers(List<User> users)
        {
            throw new NotImplementedException();
        }
    }
}