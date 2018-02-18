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
        private IMvxCommand _screenPartnerCommand;


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

        public IMvxCommand ScreenPartnerCommand
        {
            get
            {
                _screenPartnerCommand = _screenPartnerCommand ?? new MvxCommand(ScreenPartner);
                return _screenPartnerCommand;
            }
        }

        private void ScreenPartner()
        {
            PartnerViewModel.ShowDashboard(PartnerTemplate);
        }

        public string ScreenText { get; set; }
        public bool ShowScreen { get; set; }

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
            ScreenText = "Screen";
            ShowScreen = !_partnerTemplate.IsIndex;
            if (_partnerTemplate.IsIndex)
                _partnerTemplate.FullName = $"{partnerTemplate.FullName} [index]";
        }


        private void RemoveRelationship()
        {
            PartnerViewModel.RemoveRelationship(PartnerTemplate);
        }
    }
}