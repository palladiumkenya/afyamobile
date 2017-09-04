using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Wrapper
{
    public interface ITestTemplateWrap
    {
        ITestEpisodeViewModel EpisodeViewModel { get; }
        ITestTemplate TestTemplate { get; }
        IMvxCommand EditTestCommand { get; }
        IMvxCommand RemoveTestCommand { get; }
    }
}