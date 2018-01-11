using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class FamilyMemberTemplateWrap : IFamilyMemberTemplateWrap
    {
        private ClientDashboardViewModel _parent;
        private readonly IFamilyMemberViewModel _FamilyMemberViewModel;

        private FamilyMemberTemplate _FamilyMemberTemplate;
        private IMvxCommand _removeFamilyMemberCommand;


        public IFamilyMemberViewModel FamilyMemberViewModel
        {
            get { return _FamilyMemberViewModel; }
        }

        public FamilyMemberTemplate FamilyMemberTemplate
        {
            get { return _FamilyMemberTemplate; }
        }
        public IMvxCommand RemoveFamilyMemberCommand
        {
            get
            {
                _removeFamilyMemberCommand = _removeFamilyMemberCommand ?? new MvxCommand(RemoveRelationship, CanRemoveRelationship);
                return _removeFamilyMemberCommand;
            }
        }

        private bool CanRemoveRelationship()
        {
            return
                null != _FamilyMemberTemplate &&
                null != _FamilyMemberTemplate.Id;
        }

        public FamilyMemberTemplateWrap(FamilyMemberTemplate FamilyMemberTemplate, IFamilyMemberViewModel FamilyMemberViewModel)
        {
            _FamilyMemberTemplate = FamilyMemberTemplate;
            _FamilyMemberViewModel = FamilyMemberViewModel;
        }


        private void RemoveRelationship()
        {
            FamilyMemberViewModel.RemoveFamilyMember(FamilyMemberTemplate);
        }
    }
}