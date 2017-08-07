using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class PartnerItemWrap
    {
        private PartnerItem _partnerItem;
        private ClientDashboardViewModel _parent;
        private  IMvxCommand _removeRelationshipCommand;

        public PartnerItem PartnerItem
        {
            get { return _partnerItem; }
        }
        public IMvxCommand RemoveRelationshipCommand
        {
            get
            {
                _removeRelationshipCommand = _removeRelationshipCommand ?? new MvxCommand(RemoveRelationship);
                return _removeRelationshipCommand;
            }
        }
        
        public PartnerItemWrap(PartnerItem partnerItem, ClientDashboardViewModel parent)
        {
            _partnerItem = partnerItem;
            _parent = parent;
        }
        private void RemoveRelationship()
        {
            _parent.RemoveRelationship(PartnerItem);
        }
    }
}