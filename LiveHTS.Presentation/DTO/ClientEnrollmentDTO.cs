using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.DTO
{
    public class ClientEnrollmentDTO : IEnrollment
    {
        public string Id { get; set; }
        public string ClientId { get; set; }

        public Guid PracticeId { get; set; }
        public string IdentifierTypeId { get; set; }
        public string Identifier { get; set; }
        public DateTime RegistrationDate { get; set; }

        public bool HasAnyData
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Identifier) ||
                       !string.IsNullOrWhiteSpace(IdentifierTypeId);
            }
        }

        public ClientEnrollmentDTO()
        {
        }

        private ClientEnrollmentDTO(Guid practiceId, string identifierTypeId, string identifier,
            DateTime registrationDate)
        {
            PracticeId = practiceId;
            IdentifierTypeId = identifierTypeId;
            Identifier = identifier;
            RegistrationDate = registrationDate;
        }

        public static ClientEnrollmentDTO CreateFromView(ClientEnrollmentViewModel clientEnrollmentViewModel)
        {
            
            var enrollmentDTO= new ClientEnrollmentDTO(clientEnrollmentViewModel.SelectedPractice.Id,
                clientEnrollmentViewModel.SelectedIdentifierType.Id, clientEnrollmentViewModel.Identifier,
                clientEnrollmentViewModel.RegistrationDate);

            enrollmentDTO.ClientId = clientEnrollmentViewModel.ClientId;
            enrollmentDTO.Id = clientEnrollmentViewModel.Id;
            return enrollmentDTO;
        }

        public static ClientEnrollmentDTO CreateFromClient(Client client)
        {
            var enrollmentDTO = new ClientEnrollmentDTO();

            if (null != client)
            {
                enrollmentDTO.PracticeId = client.PracticeId;
                

                //Client Identifiers

                if (client.Identifiers.Any())
                {
                    var clientIdentifier = client.Identifiers.First();
                    enrollmentDTO.IdentifierTypeId = clientIdentifier.IdentifierTypeId;
                    enrollmentDTO.Identifier = clientIdentifier.Identifier;
                    enrollmentDTO.RegistrationDate = clientIdentifier.RegistrationDate;
                    enrollmentDTO.ClientId = clientIdentifier.ClientId.ToString();
                    enrollmentDTO.Id = clientIdentifier.Id.ToString();
                }
                else
                {
                    enrollmentDTO.ClientId = client.Id.ToString();
                    enrollmentDTO.RegistrationDate =DateTime.Today;
                }
            }
            return enrollmentDTO;
        }
    }
}