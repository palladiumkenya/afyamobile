using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Wrapper
{
    public interface IPartnerTemplateWrap
    {
        IPartnerViewModel PartnerViewModel { get; }
        PartnerTemplate PartnerTemplate { get; }
        IMvxCommand RemovePartnerCommand { get; }
        IMvxCommand ScreenPartnerCommand { get; }
        string ScreenText { get; set; }
        bool ShowScreen { get; set; }
    }
}