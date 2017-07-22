using System.Collections.Generic;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services
{
    public interface IAppDashboardService
    {
        AppDashboard Load(User user,string serial);
    }
}