using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class RelationshipTemplateWrap : IRelationshipTemplateWrap
    {
        private ClientDashboardViewModel _parent;

        private RelationshipTemplate _relationshipTemplate;
        private  IMvxCommand _removeRelationshipCommand;
        private readonly IPartnerViewModel _partnerViewModel;

        public IPartnerViewModel PartnerViewModel
        {
            get { return _partnerViewModel; }
        }

        public RelationshipTemplate RelationshipTemplate
        {
            get { return _relationshipTemplate; }
        }
        public IMvxCommand RemoveRelationshipCommand
        {
            get
            {
                _removeRelationshipCommand = _removeRelationshipCommand ?? new MvxCommand(RemoveRelationship, CanRemoveRelationship);
                return _removeRelationshipCommand;
            }
        }

        private bool CanRemoveRelationship()
        {
            return
                null != _relationshipTemplate &&
                null != _relationshipTemplate.Id;
        }

        public RelationshipTemplateWrap(RelationshipTemplate relationshipTemplate, IPartnerViewModel partnerViewModel)
        {
            _relationshipTemplate = relationshipTemplate;
            _partnerViewModel = partnerViewModel;
        }


        public RelationshipTemplateWrap(RelationshipTemplate relationshipTemplate, ClientDashboardViewModel parent)
        {
            _relationshipTemplate = relationshipTemplate;
            _parent = parent;
        }


        private void RemoveRelationship()
        {
            _parent.RemoveRelationship(RelationshipTemplate);
        }
    }
}