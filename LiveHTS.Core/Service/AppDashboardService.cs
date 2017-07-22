using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Service
{
    public class AppDashboardService:IAppDashboardService
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IDeviceRepository _deviceRepository;

        private  User _user;

        public AppDashboardService(IModuleRepository moduleRepository, IDeviceRepository deviceRepository)
        {
            _moduleRepository = moduleRepository;
            _deviceRepository = deviceRepository;
        }

        public AppDashboard Load(User user,string serial)
        {
            _user = user;

            var module = _moduleRepository.GetDefaultModule();

            var device = _deviceRepository.GetBySerial(serial);

            var dashboard= new AppDashboard(module,user,device);

            return dashboard;
        }
    }
}