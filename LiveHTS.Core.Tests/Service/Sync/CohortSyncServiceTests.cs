using System;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Service.Sync;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHTS.Core.Tests.Service.Sync
{
    [TestFixture]
    public class CohortSyncServiceTests
    {
        private readonly string _local = "http://DESKTOP-VO0N4SS:6000/api/activate/local";
        private ICohortSyncService _cohortSyncService;
        [SetUp]
        public void SetUp()
        {

            _cohortSyncService =new CohortSyncService(new RestClient());
        }

        [Test]
        public void should_load_counties()
        {
            var cohorts = _cohortSyncService.GetCohorts(_local).Result;

            Assert.IsTrue(cohorts.Count>0);
            foreach (var cohort in cohorts)
            {
                Console.WriteLine(cohort);
            }
        }
    }

    
}