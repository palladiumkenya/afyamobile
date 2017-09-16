using LiveHTS.Core.Model.Config;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IPushDataViewModel
    {
        Device Device { get; set; }
        ServerConfig Local { get; set; }
        bool IsBusy { get; set; }
        string Address { get; set; }
        string CurrentStatus { get; set; }
        int CurrentStatusProgress { get; set; }
        string OverallStatus { get; set; }
        int OverallStatusProgress { get; set; }
        IMvxCommand PushDataCommand { get; }
    }
}