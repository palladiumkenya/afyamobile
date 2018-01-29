using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Services.Config
{
    public interface ISetupWizardService
    {
        void UpdateDevice(Guid practiceId);
        void UpdateServerConfig(ServerConfig config);
        void UpdateUsers(List<User> users);
    }
}