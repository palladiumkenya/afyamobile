using LiveHTS.Core.Model.Config;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IDeviceViewModel
    {
        Device Device { get; set; }
         string Serial { get; set; }
         string Code { get; set; }
         string Name { get; set; }
        IMvxCommand SaveDeviceCommand { get; }

        void LoadDeviceInfo(string serial, string name, string manufacturer);
    }
}