using System;
using System.IO;
using System.Linq;
using LiveHTS.Presentation.DTO;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHTS.Presentation.Tests.DTO
{
    [TestFixture]
    public class ClientRegistrationDTOTests
    {
        private ClientRegistrationDTO _clientRegistrationDTO;

        private ClientDemographicDTO _demographicDTO;
        private ClientContactAddressDTO _addressDTO;
        private ClientProfileDTO _profileDTO;
        private ClientEnrollmentDTO _enrollmentDTO;

        [SetUp]
        public void SetUp()
        {
            _demographicDTO = JsonConvert.DeserializeObject<ClientDemographicDTO>(GetJson("d"));
            _addressDTO = JsonConvert.DeserializeObject<ClientContactAddressDTO>(GetJson("c"));
            _profileDTO = JsonConvert.DeserializeObject<ClientProfileDTO>(GetJson("p"));
            _enrollmentDTO = JsonConvert.DeserializeObject<ClientEnrollmentDTO>(GetJson("e"));

            _clientRegistrationDTO=new ClientRegistrationDTO(_demographicDTO,_addressDTO,_profileDTO,_enrollmentDTO);
        }

        [Test]
        public void should_Generate_ClientRegistrationDTO()
        {
            Assert.IsNotNull(_clientRegistrationDTO);
            Assert.IsNotNull(_clientRegistrationDTO.ClientDemographic);
            Assert.IsNotNull(_clientRegistrationDTO.ClientContactAddress);
            Assert.IsNotNull(_clientRegistrationDTO.ClientProfile);
            Assert.IsNotNull(_clientRegistrationDTO.ClientEnrollment);
        }

        [Test]
        public void should_Generate_Client_From_DTO()
        {
            var person = _clientRegistrationDTO.GeneratePerson();
            Assert.IsNotNull(person);
            Assert.IsNotNull(person.Person);
            Assert.IsNotNull(person.Person.Addresses.FirstOrDefault());
            Assert.IsNotNull(person.Person.Contacts.FirstOrDefault());
            Assert.IsNotNull(person.Identifiers.FirstOrDefault());
            Assert.IsNotNull(person.Relationships.FirstOrDefault());
            Console.WriteLine(person);
        }

        [Test]
        public void should_Generate_Client_From_DTO()
        {
            var person = _clientRegistrationDTO. ();
            Assert.IsNotNull(person);
            Assert.IsNotNull(person.Person);
            Assert.IsNotNull(person.Person.Addresses.FirstOrDefault());
            Assert.IsNotNull(person.Person.Contacts.FirstOrDefault());
            Assert.IsNotNull(person.Identifiers.FirstOrDefault());
            Assert.IsNotNull(person.Relationships.FirstOrDefault());
            Console.WriteLine(person);
        }


        private string GetJson(string fileName)
        {
            var jsonPath = $@"{TestContext.CurrentContext.TestDirectory.HasToEndWith(@"\")}DTOJsons";
            var json = $@"{jsonPath.HasToEndWith(@"\")}{fileName}.json";
            var fileContent = File.ReadAllText(json);
            return fileContent;
        }
    }
}