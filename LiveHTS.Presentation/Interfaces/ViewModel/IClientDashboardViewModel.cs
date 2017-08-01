using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientDashboardViewModel
    {
        Client Client { get; set; }
        bool IsBusy { get; set; }
    }
}