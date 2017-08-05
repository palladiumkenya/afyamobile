using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientDashboardViewModel
    {
        Client Client { get; set; }
        IMvxCommand ManageRegistrationCommand { get; }
        IMvxCommand AddRelationShipCommand { get; }
        IMvxCommand RemoveRelationShipCommand { get; }
        Client SeletctedRelationShip { get; set; }
        bool IsBusy { get; set; }
        void ShowRegistry();
    }
}