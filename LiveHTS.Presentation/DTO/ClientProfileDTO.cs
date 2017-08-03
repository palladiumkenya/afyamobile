using System.Linq;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.DTO
{
    public class ClientProfileDTO:IProfile
    {
        public string ClientId { get; set; }

        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string OtherKeyPop { get; set; }

        public bool HasAnyData
        {
            get
            {
                return !string.IsNullOrWhiteSpace(MaritalStatus) ||
                       !string.IsNullOrWhiteSpace(KeyPop);
            }
        }

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
            var profileDTO= new ClientProfileDTO(clientProfileViewModel.SelectedMaritalStatus.Id, clientProfileViewModel.SelectedKeyPop.Id, clientProfileViewModel.OtherKeyPop);

            profileDTO.ClientId = clientProfileViewModel.ClientId;
            return profileDTO;
        }

        public static ClientProfileDTO CreateFromClient(Client client)
        {
            var profileDTO = new ClientProfileDTO();

            if (null != client)
            {
                profileDTO.MaritalStatus = client.MaritalStatus;
                profileDTO.KeyPop = client.KeyPop;
                profileDTO.OtherKeyPop = client.OtherKeyPop;
                profileDTO.ClientId = client.Id.ToString();
            }

            return profileDTO;
        }
    }
}