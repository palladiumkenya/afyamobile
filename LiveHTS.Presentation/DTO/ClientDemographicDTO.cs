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
        public decimal Age { get; set; }
        public string AgeUnit { get; set; }
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

        private ClientDemographicDTO(string firstName, string middleName, string lastName, string gender, decimal age, string ageUnit, DateTime? birthDate):this()
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            Age = age;
            AgeUnit = ageUnit;
            BirthDate = birthDate;
        }

        public static ClientDemographicDTO CreateFromView(IClientDemographicViewModel model)
        {
            return new ClientDemographicDTO(
                model.FirstName,
                model.MiddleName,
                model.LastName,
                model.SelectedGender.Value,
                model.Age,
                model.SelectedAgeUnit.Value,
                model.BirthDate);
        }

        public override string ToString()
        {
            return $"{FullName},{Gender}";
        }
    }
}