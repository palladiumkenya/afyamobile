using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Wrapper
{
    public interface IFamilyTraceTemplateWrap
    {
        IMemberTracingViewModel Parent { get; }

        IFamilyTraceTemplate TraceTemplate { get; }
        IMvxCommand EditTraceCommand { get; }
        IMvxCommand DeleteTraceCommand { get; }
    }
}