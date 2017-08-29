using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Wrapper
{
    public interface ITraceTemplateWrap
    {
        ILinkageViewModel Parent { get; }
        ITraceTemplate TraceTemplate { get; }
        IMvxCommand SaveTraceCommand { get; }
        IMvxCommand DeleteTraceCommand { get; }
        IMvxCommand ShowDateDialogCommand { get; }
    }
}