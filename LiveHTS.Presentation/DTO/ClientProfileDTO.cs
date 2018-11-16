using System;
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
        public Guid? Education { get; set; }
        public Guid? Completion { get; set; }
        public Guid? Occupation { get; set; }
        public string RelTypeId { get; set; }
        public bool? PreventEnroll { get; set; }
        
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
        private ClientProfileDTO(string maritalStatus, string keyPop, string otherKeyPop,string relTypeId, Guid? education, Guid? completion, Guid? occupation)
        {
            MaritalStatus = maritalStatus;
            KeyPop = keyPop;
            OtherKeyPop = otherKeyPop;
            RelTypeId = relTypeId;
            Education = education;
            Completion = completion;
            Occupation = occupation;
        }

        public static ClientProfileDTO CreateFromView(ClientProfileViewModel clientProfileViewModel)
        {
            var relTypeId = null != clientProfileViewModel.SelectedRelationshipType
                ? clientProfileViewModel.SelectedRelationshipType.Id
                : "";

            var profileDTO= new ClientProfileDTO(clientProfileViewModel.SelectedMaritalStatus.Id, clientProfileViewModel.SelectedKeyPop.Id, clientProfileViewModel.OtherKeyPop, relTypeId, 
                clientProfileViewModel.SelectedEducation?.ItemId, clientProfileViewModel.SelectedCompletion?.ItemId,clientProfileViewModel.SelectedOccupation?.ItemId);
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
                profileDTO.PreventEnroll = client.PreventEnroll;
                profileDTO.Education = client.Education;
                profileDTO.Completion = client.Completion;
                profileDTO.Occupation = client.Occupation;
            }

            return profileDTO;
        }
    }
}