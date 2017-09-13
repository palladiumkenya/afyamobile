using System;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Service.Sync;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHTS.Core.Tests.Service.Sync
{
    [TestFixture]
    public class MetaSyncServiceTests
    {
        private IMetaSyncService _metaSyncService;
        [SetUp]
        public void SetUp()
        {

            _metaSyncService =new MetaSyncService(new RestClient());
        }

        [Test]
        public void should_load_counties()
        {
            var counties = _metaSyncService.GetAll().Result;

            Assert.IsTrue(counties.Count>0);
            foreach (var county in counties)
            {
                Console.WriteLine(county);
            }
        }
    }

    
}