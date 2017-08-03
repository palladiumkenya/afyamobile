using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Presentation.DTO;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientProfileViewModel : IStepViewModel
    {
        ClientProfileDTO Profile { get; set; }
        string ClientInfo { get; set; }
        IEnumerable<MaritalStatus> MaritalStatus { get; set; }
        IEnumerable<KeyPop> KeyPops { get; set; }
        MaritalStatus SelectedMaritalStatus { get; set; }
        KeyPop SelectedKeyPop { get; set; }
        string IsOtherKeyPop { get; set; }
        string OtherKeyPop { get; set; }
        string ClientId { get; set; }
        string Id { get; set; }
    }
}