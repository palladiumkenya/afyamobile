using System;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.DTO
{
    public class ClientEnrollmentDTO:IEnrollment
    {
        public Guid PracticeId { get; set; }
        public string IdentifierTypeId { get; set; }
        public string Identifier { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ClientEnrollmentDTO()
        {
        }

        private ClientEnrollmentDTO(Guid practiceId, string identifierTypeId, string identifier, DateTime registrationDate)
        {
            PracticeId = practiceId;
            IdentifierTypeId = identifierTypeId;
            Identifier = identifier;
            RegistrationDate = registrationDate;
        }

        public static ClientEnrollmentDTO CreateFromView(ClientEnrollmentViewModel clientEnrollmentViewModel)
        {
            return new ClientEnrollmentDTO(clientEnrollmentViewModel.SelectedPractice.Id, clientEnrollmentViewModel.SelectedIdentifierType.Id, clientEnrollmentViewModel.Identifier, clientEnrollmentViewModel.RegistrationDate);
        }
    }
}