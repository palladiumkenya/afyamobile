using System;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.DTO
{
    public class ClientDemographicDTO:IPerson
    {
        public string PersonId { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
        public decimal Age { get; set; }
        public string AgeUnit { get; set; }
        public DateTime BirthDate { get; set; }
        public bool? BirthDateEstimated { get; set; }
        public bool Downloaded { get; set; }

        public bool HasAnyData
        {
            get
            {
                return !string.IsNullOrWhiteSpace(FirstName) ||
                       !string.IsNullOrWhiteSpace(LastName);
            }
        }

        public virtual string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }

        public ClientDemographicDTO()
        {
            BirthDateEstimated = false;
        }
        private ClientDemographicDTO(string firstName, string middleName, string lastName, string gender, DateTime birthDate, string nickName) : this()
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            NickName = nickName;
            Gender = gender;

            BirthDate = birthDate;
        }

        private ClientDemographicDTO(string firstName, string middleName, string lastName, string gender, DateTime birthDate, decimal age, string ageUnit, string nickName) :this(firstName, middleName,  lastName, gender, birthDate, nickName)
        {
            Age = age;
            AgeUnit = ageUnit;
        }

        public static ClientDemographicDTO CreateFromView(ClientDemographicViewModel model)
        {

            var demographicDTO= new ClientDemographicDTO(
                model.FirstName,
                model.MiddleName,
                model.LastName,
                model.SelectedGender.Value,
                model.BirthDate,
                model.Age,
                model.SelectedAgeUnit.Value,
                model.NickName
            );
            demographicDTO.Downloaded = model.Downloaded;
            demographicDTO.PersonId = model.PersonId;
            return demographicDTO;
        }

        public static ClientDemographicDTO CreateFromClient(Client client,bool downloaded)
        {
            var demographicDTO=new ClientDemographicDTO();

            //Person

            if (null != client)
            {
                if (null != client.Person)
                {
                    var model = client.Person;
                    demographicDTO= new ClientDemographicDTO(
                        model.FirstName,
                        model.MiddleName,
                        model.LastName,
                        model.Gender,
                        model.BirthDate,
                        model.NickName);

                    demographicDTO.PersonId = model.Id.ToString();
                    demographicDTO.Downloaded = downloaded;
                }
            }


            return demographicDTO;
        }

        public override string ToString()
        {
            return $"{FullName},{Gender}";
        }
    }
}
