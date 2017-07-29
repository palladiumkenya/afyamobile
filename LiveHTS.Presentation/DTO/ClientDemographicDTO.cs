using System;
using LiveHTS.Core.Interfaces.Model;
using MvvmValidation;

namespace LiveHTS.Presentation.DTO
{
    public class ClientDemographicDTO:IPerson
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? BirthDateEstimated { get; set; }

        public ClientDemographicDTO()
        {
            
            BirthDateEstimated = false;
        }
    }
}