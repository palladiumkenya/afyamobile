using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface ISyncDeviceService
    {
        void EnrollDevice();
        Practice EnrollPractice();

    }
}