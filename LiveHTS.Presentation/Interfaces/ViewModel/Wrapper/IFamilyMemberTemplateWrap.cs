using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Wrapper
{
    public interface IFamilyMemberTemplateWrap
    {
        IFamilyMemberViewModel FamilyMemberViewModel { get; }
        FamilyMemberTemplate FamilyMemberTemplate { get; }
        IMvxCommand RemoveFamilyMemberCommand { get; }
    }
}