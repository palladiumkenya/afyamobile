using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Presentation.DTO;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientProfileViewModel : IStepViewModel
    {
        ClientContactAddressDTO ContactAddress { get; }
        string ClientInfo { get; set; }

        IEnumerable<MaritalStatus> MaritalStatus { get; set; }
        IEnumerable<KeyPop> KeyPops { get; set; }
        MaritalStatus SelectedMaritalStatus { get; set; }
        KeyPop SelectedKeyPop { get; set; }
        string IsOtherKeyPop { get; set; }
        string OtherKeyPop { get; set; }
    }
}