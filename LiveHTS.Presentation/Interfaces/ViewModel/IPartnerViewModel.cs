using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IPartnerViewModel: IMvxViewModel
    {
        IDashboardViewModel Parent { get; set; }
        string Title { get; set; }
        Client Client { get; set; }
        List<PartnerTemplateWrap> Partners { get; set; }

        IMvxCommand AddPartnerCommand { get; }

        void RemoveRelationship(PartnerTemplate template);
        void ShowDashboard(PartnerTemplate template);
    }
}