using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientDashboardViewModel
    {
        Module Module { get; set; }
        IEnumerable<Form> Forms { get; set; }
        Client Client { get; set; }
        IMvxCommand ManageRegistrationCommand { get; }
        IMvxCommand AddRelationShipCommand { get; }
        IMvxCommand RemoveRelationShipCommand { get; }
        Client SeletctedRelationShip { get; set; }
        IMvxCommand StartEncounterCommand { get; }

        bool IsBusy { get; set; }
        void ShowRegistry();
    }
}