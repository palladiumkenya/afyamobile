using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Wrapper
{
    public interface IHIVTestTemplateWrap
    {
        IClientHIVTestViewModel Parent { get; }
        IHIVTestTemplate HIVTestTemplate { get; }
        IMvxCommand SaveTestCommand { get; }
        IMvxCommand DeleteTestCommand { get; }
        IMvxCommand ShowDateDialogCommand { get; }
    }
}