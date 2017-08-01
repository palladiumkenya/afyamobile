using System;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Presentation.Interfaces.ViewModel;

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

        public virtual string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }

        public ClientDemographicDTO()
        {
            BirthDateEstimated = false;
        }

        private ClientDemographicDTO(string firstName, string middleName, string lastName, string gender, DateTime? birthDate):this()
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
        }

        public static ClientDemographicDTO CreateFromView(IClientDemographicViewModel model)
        {
            return new ClientDemographicDTO(
                model.FirstName, 
                model.MiddleName, 
                model.LastName, 
                model.Gender, 
                model.BirthDate);
        }

        public override string ToString()
        {
            return $"{FullName},{Gender}";
        }
    }
}