using LiveHTS.Core.Model.Config;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IDeviceViewModel
    {
        Device Device { get; set; }
        ServerConfig Central { get; set; }
        ServerConfig Local { get; set; }
        string Serial { get; set; }
         string Code { get; set; }
         string Name { get; set; }

        string CentralAddress { get; set; }
        string CentralName { get; set; }

        string LocalAddress { get; set; }
        string LocalName { get; set; }

        IMvxCommand SaveDeviceCommand { get; }
        IMvxCommand VerifyCentralCommand { get; }
        IMvxCommand VerifyLocalCommand { get; }

        void LoadDeviceInfo(string serial, string name, string manufacturer);
    }
}