using LiveHTS.Presentation.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Wrapper
{
    public interface IRelationshipTemplateWrap
    {
        IPartnerViewModel PartnerViewModel { get; }
        RelationshipTemplate RelationshipTemplate { get; }
        IMvxCommand RemoveRelationshipCommand { get; }
    }
}