using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Services.Config
{
    public interface IDeviceSetupService
    {
        bool IsSetup();
        Device GetDefault(Guid deviceId);
        Device GetDefault(string serial="");
        ServerConfig GetCentral();
        ServerConfig GetLocal();
        void SaveCentral(ServerConfig config);
        void SaveLocal(ServerConfig config);
        void Register(Device device);
        void CheckRegister(Device device);
        void SaveUsers(List<User> users);
    }
}