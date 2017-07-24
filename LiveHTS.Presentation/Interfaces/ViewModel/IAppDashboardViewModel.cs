using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IAppDashboardViewModel
    {
        IMvxCommand RegistryCommand { get; }
        string Profile { get; set; }
    }
}