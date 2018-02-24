using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IFamilyMemberViewModel : IMvxViewModel
    {
        IDashboardViewModel Parent { get; set; }
        string Title { get; set; }
        Client Client { get; set; }
        bool EnableAddFamily { get; set; }
        List<FamilyMemberTemplateWrap> FamilyMembers { get; set; }

        IMvxCommand AddFamilyMemberCommand { get; }
        
        void RemoveFamilyMember(FamilyMemberTemplate template);
        void ShowDashboard(FamilyMemberTemplate template);
    }
}