using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Presentation.DTO;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientEnrollmentViewModel : IStepViewModel
    {
        ClientEnrollmentDTO Enrollment { get; set; }
        string ClientInfo { get; set; }
        IEnumerable<IdentifierType> IdentifierTypes { get; set; }
        IEnumerable<Practice> Practices { get; set; }
        Practice SelectedPractice { get; set; }
        IdentifierType SelectedIdentifierType { get; set; }
        string Identifier { get; set; }
        DateTime RegistrationDate { get; set; }
        string ClientId { get; set; }
    }
}