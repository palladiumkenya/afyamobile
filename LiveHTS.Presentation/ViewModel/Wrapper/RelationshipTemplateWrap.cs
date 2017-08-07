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

        public RelationshipTemplate RelationshipTemplate
        {
            get { return _relationshipTemplate; }
        }
        public IMvxCommand RemoveRelationshipCommand
        {
            get
            {
                _removeRelationshipCommand = _removeRelationshipCommand ?? new MvxCommand(RemoveRelationship);
                return _removeRelationshipCommand;
            }
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