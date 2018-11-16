using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ISetupWizardViewModel
    {
        Device Device { get; set; }
        ServerConfig Local { get; set; }
     
        string Serial { get; set; }
        string Name { get; set; }
        string Emr { get; set;}
        string Url { get; set; }
        string Facility { get; set; }
        string Status { get; set; }
        bool Loading { get; set; }
        string SetupAction { get; set; }

        IEnumerable<Practice> Practices { get; set; }
        Practice SelectedPractice { get; set; }

        IMvxCommand SetupDeviceCommand { get; }
        IMvxCommand LoginCommand { get; }
        void LoadDeviceInfo(string serial, string name, string manufacturer);
    }
}