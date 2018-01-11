using System;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Config
{
    public interface IDeviceSetupService
    {
        Device GetDefault(Guid deviceId);
        Device GetDefault(string serial="");
        ServerConfig GetCentral();
        ServerConfig GetLocal();
        void SaveCentral(ServerConfig config);
        void SaveLocal(ServerConfig config);
        void Register(Device device);
        void CheckRegister(Device device);
    }
}