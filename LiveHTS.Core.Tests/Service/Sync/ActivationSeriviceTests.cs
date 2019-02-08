using System;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Service.Sync;
using NUnit.Framework;

namespace LiveHTS.Core.Tests.Service.Sync
{
    [TestFixture]
    public class ActivationSeriviceTests
    {
        private readonly string _central = "http://data.kenyahmis.org:6000/api/activate/central";
        private readonly string _local = "http://DESKTOP-VO0N4SS:6000/api/activate/local";

        private IActivationService _activationService;
        [SetUp]
        public void SetUp()
        {

            _activationService =new ActivationService(new RestClient());
        }

        [Test]
        public void should_load_Central()
        {
            var practice = _activationService.GetCentral(_central).Result;
            Assert.IsNotNull(practice);
            Console.WriteLine(practice);
        }
        [Test]
        public void should_load_Local()
        {
            var practice = _activationService.GetLocal(_local).Result;
            Assert.IsNotNull(practice);
            Console.WriteLine(practice);
        }
    }
}