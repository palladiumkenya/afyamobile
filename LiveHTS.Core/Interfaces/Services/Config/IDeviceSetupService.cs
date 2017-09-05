using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Config
{
    public interface IDeviceSetupService
    {
        Device GetDefault(string serial="");
        void Register(Device device);
    }
}