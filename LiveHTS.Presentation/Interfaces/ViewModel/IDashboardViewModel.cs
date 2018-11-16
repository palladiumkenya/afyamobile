using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IDashboardViewModel
    {
        int GetActiveTab();
        IEncounterViewModel EncounterViewModel { get; }
        IFamilyMemberViewModel FamilyMemberViewModel { get; }
        IPartnerViewModel PartnerViewModel { get; }
        ISummaryViewModel SummaryViewModel { get; }
        bool ShowEnroll { get; set; }
        bool ShowWriteToCard { get; set; }
        IndexClientDTO IndexClient { get; set; }
        Client Client { get; set; }
        Module Module { get; set; }
        List<Module> Modules { get; set; }

        IMvxCommand ManageRegistrationCommand { get;  }
        IMvxCommand EnrollCommand { get; }
        IMvxCommand SmartCardCommand { get; }
        void GoBack();
    }
}