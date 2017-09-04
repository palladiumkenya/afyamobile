using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class PartnerTemplateWrap : IPartnerTemplateWrap
    {
        private ClientDashboardViewModel _parent;
        private readonly IPartnerViewModel _partnerViewModel;

        private PartnerTemplate _partnerTemplate;
        private  IMvxCommand _removePartnerCommand;
     

        public IPartnerViewModel PartnerViewModel
        {
            get { return _partnerViewModel; }
        }

        public PartnerTemplate PartnerTemplate
        {
            get { return _partnerTemplate; }
        }
        public IMvxCommand RemovePartnerCommand
        {
            get
            {
                _removePartnerCommand = _removePartnerCommand ?? new MvxCommand(RemoveRelationship, CanRemoveRelationship);
                return _removePartnerCommand;
            }
        }

        private bool CanRemoveRelationship()
        {
            return
                null != _partnerTemplate &&
                null != _partnerTemplate.Id;
        }

        public PartnerTemplateWrap(PartnerTemplate partnerTemplate, IPartnerViewModel partnerViewModel)
        {
            _partnerTemplate = partnerTemplate;
            _partnerViewModel = partnerViewModel;
        }


        private void RemoveRelationship()
        {
            PartnerViewModel.RemoveRelationship(PartnerTemplate);
        }
    }
}