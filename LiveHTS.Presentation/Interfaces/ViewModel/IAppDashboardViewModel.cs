using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IAppDashboardViewModel
    {
        IMvxCommand RegistryCommand { get; }
        IMvxCommand RegisterNewClientCommand { get; }
        string Profile { get; set; }
        bool IsBusy { get; set; }
    }
}