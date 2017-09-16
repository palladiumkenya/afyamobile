using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IAppDashboardViewModel
    {
        IMvxCommand RegistryCommand { get; }
        IMvxCommand RegisterNewClientCommand { get; }
        IMvxCommand QuitCommand { get; }
        IMvxCommand DeviceCommand { get; }
        IMvxCommand PracticeCommand { get; }
        IMvxCommand PullDataCommand { get; }
        IMvxCommand PushDataCommand { get; }
        string Profile { get; set; }
        bool IsBusy { get; set; }
    }
}