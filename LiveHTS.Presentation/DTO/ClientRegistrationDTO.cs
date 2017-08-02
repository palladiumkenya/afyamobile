using System;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.DTO
{
    public class ClientRegistrationDTO
    {
        public ClientDemographicDTO ClientDemographic { get; set; }
        public ClientContactAddressDTO ClientContactAddress { get; set; }
        public ClientProfileDTO ClientProfile { get; set; }
        public ClientEnrollmentDTO ClientEnrollment { get; set; }

        public ClientRegistrationDTO(ISettings settings)
        {
            var demographic = settings.GetValue(nameof(ClientDemographicViewModel), "");
            var contactAddress = settings.GetValue(nameof(ClientContactViewModel), "");
            var profile = settings.GetValue(nameof(ClientProfileViewModel), "");
            var enrollment = settings.GetValue(nameof(ClientEnrollmentViewModel), "");

            if (!string.IsNullOrWhiteSpace(demographic ))
                ClientDemographic =  JsonConvert.DeserializeObject<ClientDemographicDTO>(demographic );
            if (!string.IsNullOrWhiteSpace(contactAddress))
                ClientContactAddress = JsonConvert.DeserializeObject<ClientContactAddressDTO>(contactAddress);
            if (!string.IsNullOrWhiteSpace(profile))
                ClientProfile = JsonConvert.DeserializeObject<ClientProfileDTO>(profile);
            if (!string.IsNullOrWhiteSpace(enrollment))
                ClientEnrollment = JsonConvert.DeserializeObject<ClientEnrollmentDTO>(enrollment);
        }
        public ClientRegistrationDTO(ClientDemographicDTO clientDemographic, ClientContactAddressDTO clientContactAddress, ClientProfileDTO clientProfile, ClientEnrollmentDTO clientEnrollment)
        {
            ClientDemographic = clientDemographic;
            ClientContactAddress = clientContactAddress;
            ClientProfile = clientProfile;
            ClientEnrollment = clientEnrollment;
        }

        public Client Generate()
        {
            //Person
            var person = GeneratePerson();

            //Client
            var client = GenerateClient(person);

            return client;
        }
        private Client GenerateClient(Person person)
        {
            var client = GenerateClient(person.Id);
            client.Person = person;
            return client;
        }

        private Client GenerateClient(Guid personId)
        {
            //ClientIdentifier 

            //string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Person person

            var client = Client.CreateFromPerson(ClientProfile.MaritalStatus, ClientProfile.KeyPop, ClientProfile.OtherKeyPop, ClientEnrollment.PracticeId,personId);

            var clientIdentifier = GenerateClientIdentifier(client.Id);
            if (null != clientIdentifier)
                client.Identifiers.ToList().Add(clientIdentifier);

            return client;
        }
        private ClientIdentifier GenerateClientIdentifier(Guid clientId)
        {
            //ClientIdentifier 

            //string identifierTypeId, string identifier, DateTime registrationDate,bool preferred, Guid clientId

            var clientIdentifier = ClientIdentifier.Create(ClientEnrollment.IdentifierTypeId, ClientEnrollment.Identifier, ClientEnrollment.RegistrationDate,true,clientId);

            return clientIdentifier;
        }

        public Person GeneratePerson()
        {
            //Person 

            //string firstName, string middleName, string lastName, string gender,DateTime? birthDate, bool? birthDateEstimated, string email

            var person = Person.Create(ClientDemographic.FirstName, ClientDemographic.MiddleName,
                ClientDemographic.LastName, ClientDemographic.Gender, ClientDemographic.BirthDate,
                ClientDemographic.BirthDateEstimated, string.Empty);

            var contact = GeneratePersonContact(person.Id);
            if (null != contact)
                person.Contacts.ToList().Add(contact);

            var address = GeneratePersonAddress(person.Id);
            if (null != address)
                person.Addresses.ToList().Add(address);

            return person;
        }

        private PersonContact GeneratePersonContact(Guid personId)
        {
            //Person Contact

            //int? phone, bool preferred, Guid personId

            var personContact = PersonContact.Create(ClientContactAddress.Phone, true, personId);

            return personContact;
        }

        private PersonAddress GeneratePersonAddress(Guid personId)
        {
            //Person Address 

            //string landmark, Guid? countyId, bool preferred, decimal? lat, decimal? lng, Guid personId

            var personAddress = PersonAddress.Create(ClientContactAddress.Landmark, ClientContactAddress.CountyId, ClientContactAddress.Preferred, ClientContactAddress.Lat, ClientContactAddress.Lng, personId);

            return personAddress;
        }

        public void ClearCache(ISettings settings)
        {
            settings.DeleteValue(nameof(ClientDemographicDTO));
            settings.DeleteValue(nameof(ClientContactAddressDTO));
            settings.DeleteValue(nameof(ClientProfileDTO));
            settings.DeleteValue(nameof(ClientEnrollmentDTO));
        }
    }
}