using System.IO;
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
        private ClientDemographicDTO _demographicDTO;
        private ClientContactAddressDTO _addressDTO;
        private ClientProfileDTO _profileDTO;
        private ClientEnrollmentDTO _enrollmentDTO;

        [SetUp]
        public void SetUp()
        {
            _demographicDTO = JsonConvert.DeserializeObject<ClientDemographicDTO>(GetJson("d"));
        }

        [Test]
        public void should_Generate_Cleint()
        {
          Assert.IsNotNull(_demographicDTO);
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