using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IDashboardViewModel
    {
        IEncounterViewModel EncounterViewModel { get; }
        IPartnerViewModel PartnerViewModel { get; }
        ISummaryViewModel SummaryViewModel { get; }

        Client Client { get; set; }
        Module Module { get; set; }
        IMvxCommand ManageRegistrationCommand { get;  }
    }
}