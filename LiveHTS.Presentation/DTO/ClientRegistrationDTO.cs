using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel;
using LiveHTS.SharedKernel.Custom;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.DTO
{
    public class ClientRegistrationDTO
    {
        public string PersonId { get; set; }
        public string ClientId { get; set; }

        public ClientDemographicDTO ClientDemographic { get; set; }
        public ClientContactAddressDTO ClientContactAddress { get; set; }
        public ClientProfileDTO ClientProfile { get; set; }
        public ClientEnrollmentDTO ClientEnrollment { get; set; }
        public bool Downloaded { get; set; }

        public ClientRegistrationDTO()
        {
        }
        public ClientRegistrationDTO(ISettings settings,bool isClient=true)
        {
            string demographic = null;
            string contactAddress = null;
            string profile = null;
            string enrollment = null;

            if (settings.Contains(nameof(ClientDemographicViewModel)))
                demographic = settings.GetValue(nameof(ClientDemographicViewModel), "");
            if (settings.Contains(nameof(ClientContactViewModel)))
                contactAddress = settings.GetValue(nameof(ClientContactViewModel), "");
            if (settings.Contains(nameof(ClientProfileViewModel)))
                profile = settings.GetValue(nameof(ClientProfileViewModel), "");
            if (isClient)
            {
                if (settings.Contains(nameof(ClientEnrollmentViewModel)))
                    enrollment = settings.GetValue(nameof(ClientEnrollmentViewModel), "");
            }

            if (!string.IsNullOrWhiteSpace(demographic))
                ClientDemographic = JsonConvert.DeserializeObject<ClientDemographicDTO>(demographic);
            if (!string.IsNullOrWhiteSpace(contactAddress))
                ClientContactAddress = JsonConvert.DeserializeObject<ClientContactAddressDTO>(contactAddress);
            if (!string.IsNullOrWhiteSpace(profile))
                ClientProfile = JsonConvert.DeserializeObject<ClientProfileDTO>(profile);

            if (isClient)
            {
                if (!string.IsNullOrWhiteSpace(enrollment))
                    ClientEnrollment = JsonConvert.DeserializeObject<ClientEnrollmentDTO>(enrollment);
            }
        }
        public ClientRegistrationDTO(ClientDemographicDTO clientDemographic, ClientContactAddressDTO clientContactAddress, ClientProfileDTO clientProfile, ClientEnrollmentDTO clientEnrollment)
        {
            ClientDemographic = clientDemographic;
            ClientContactAddress = clientContactAddress;
            ClientProfile = clientProfile;
            ClientEnrollment = clientEnrollment;
        }

        public static ClientRegistrationDTO Create(Client client)
        {
            var clientRegistrationDto =
                new ClientRegistrationDTO
                {
                    ClientDemographic = ClientDemographicDTO.CreateFromClient(client,client.Downloaded),
                    ClientContactAddress = ClientContactAddressDTO.CreateFromClient(client,client.Downloaded),
                    ClientProfile = ClientProfileDTO.CreateFromClient(client,client.Downloaded),
                    ClientEnrollment = ClientEnrollmentDTO.CreateFromClient(client,client.Downloaded)
                };

            clientRegistrationDto.Downloaded = client.Downloaded;

            return clientRegistrationDto;
        }

        public Client Generate(Guid userId,Guid? practiceId = null)
        {
            //Person
            var person = GeneratePerson();

            //Client
            var client = GenerateClient(person,practiceId,userId);

            if (null != ClientDemographic)
                client.Downloaded = ClientDemographic.Downloaded;

            return client;
        }
        private Client GenerateClient(Person person, Guid? practiceId, Guid userId)
        {
            var client = GenerateClient(person.Id,practiceId,userId);
            client.Person = person;
            return client;
        }

        private Client GenerateClient(Guid personId,Guid? practiceId,Guid userId)
        {
            Guid clientPracticeId = practiceId.IsNullOrEmpty() ? ClientEnrollment.PracticeId : practiceId.Value;
            //ClientIdentifier

            //string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Person person


            var client = Client.CreateFromPerson(ClientProfile.MaritalStatus, ClientProfile.KeyPop,
                ClientProfile.OtherKeyPop, clientPracticeId, personId, userId, ClientProfile.Education,
                ClientProfile.Completion, ClientProfile.Occupation);

            if (null!= ClientEnrollment && !string.IsNullOrWhiteSpace(ClientEnrollment.ClientId))
            {
                client.Id = new Guid(ClientEnrollment.ClientId);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(ClientProfile.ClientId))
                {
                    client.Id = new Guid(ClientProfile.ClientId);
                }
            }


            var clientIdentifier = GenerateClientIdentifier(client.Id);
            if (null != clientIdentifier)
            {
                var clientIdentifiers=new List<ClientIdentifier>();
                clientIdentifiers.Add(clientIdentifier);
                client.Identifiers = clientIdentifiers;
            }

            return client;
        }
        private ClientIdentifier GenerateClientIdentifier(Guid clientId)
        {
            if (null == ClientEnrollment)
                return null;
            //ClientIdentifier

            //string identifierTypeId, string identifier, DateTime registrationDate,bool preferred, Guid clientId

            var clientIdentifier = ClientIdentifier.Create(ClientEnrollment.IdentifierTypeId, ClientEnrollment.Identifier, ClientEnrollment.RegistrationDate,true,clientId);

            if (!string.IsNullOrWhiteSpace(ClientEnrollment.Id))
            {
                clientIdentifier.Id = new Guid(ClientEnrollment.Id);
            }

            return clientIdentifier;
        }

        private Person GeneratePerson()
        {
            if (null == ClientDemographic)
                throw new ArgumentNullException("No Demographic data !");

            //Person

            //string firstName, string middleName, string lastName, string gender,DateTime? birthDate, bool? birthDateEstimated, string email

            var person = Person.Create(ClientDemographic.FirstName, ClientDemographic.MiddleName,
                ClientDemographic.LastName, ClientDemographic.Gender, ClientDemographic.BirthDate,
                ClientDemographic.BirthDateEstimated, string.Empty, ClientDemographic.NickName);

            if (!string.IsNullOrWhiteSpace(ClientDemographic.PersonId))
            {
                person.Id=new Guid(ClientDemographic.PersonId);
            }

            var contact = GeneratePersonContact(person.Id);
            if (null != contact)
            {
                var contacts=new List<PersonContact>();
                contacts.Add(contact);
                person.Contacts = contacts;
            }

            var address = GeneratePersonAddress(person.Id);
            if (null != address)
            {
                var addresses=new List<PersonAddress>();
                addresses.Add(address);
                person.Addresses = addresses;
            }

            return person;
        }

        private PersonContact GeneratePersonContact(Guid personId)
        {
            if (null == ClientContactAddress)
                throw new ArgumentNullException("No Contact data !");

            //Person Contact

            //int? phone, bool preferred, Guid personId

            var personContact = PersonContact.Create(ClientContactAddress.Phone, true, personId);

            if (!string.IsNullOrWhiteSpace(ClientContactAddress.ContactId))
            {
                personContact.Id = new Guid(ClientContactAddress.ContactId);
            }
            return personContact;
        }

        private PersonAddress GeneratePersonAddress(Guid personId)
        {
            if (null == ClientContactAddress)
                throw new ArgumentNullException("No Address data !");

            //Person Address

            //string landmark, Guid? countyId, bool preferred, decimal? lat, decimal? lng, Guid personId

            var personAddress = PersonAddress.Create(ClientContactAddress.Landmark, ClientContactAddress.CountyId, ClientContactAddress.Preferred, ClientContactAddress.Lat, ClientContactAddress.Lng, personId,ClientContactAddress.SubCountyId,ClientContactAddress.WardId);

            if (!string.IsNullOrWhiteSpace(ClientContactAddress.AddressId))
            {
                personAddress.Id = new Guid(ClientContactAddress.AddressId);
            }

            return personAddress;
        }

        public void ClearCache(ISettings settings)
        {

            if (settings.Contains(nameof(ClientDemographicViewModel)))
                settings.DeleteValue(nameof(ClientDemographicViewModel));

            if (settings.Contains(nameof(ClientContactViewModel)))
                settings.DeleteValue(nameof(ClientContactViewModel));

            if (settings.Contains(nameof(ClientProfileViewModel)))
                settings.DeleteValue(nameof(ClientProfileViewModel));

            if (settings.Contains(nameof(ClientEnrollmentViewModel)))
                settings.DeleteValue(nameof(ClientEnrollmentViewModel));
        }
    }
}
