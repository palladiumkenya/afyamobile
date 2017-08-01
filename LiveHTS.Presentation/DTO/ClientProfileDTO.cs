using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.DTO
{
    public class ClientProfileDTO:IProfile
    {
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string OtherKeyPop { get; set; }

        public ClientProfileDTO()
        {
        }
        private ClientProfileDTO(string maritalStatus, string keyPop, string otherKeyPop)
        {
            MaritalStatus = maritalStatus;
            KeyPop = keyPop;
            OtherKeyPop = otherKeyPop;
        }

        public static ClientProfileDTO CreateFromView(ClientProfileViewModel clientProfileViewModel)
        {
            return new ClientProfileDTO(clientProfileViewModel.SelectedMaritalStatus.Id, clientProfileViewModel.SelectedKeyPop.Id, clientProfileViewModel.OtherKeyPop);
        }
    }
}