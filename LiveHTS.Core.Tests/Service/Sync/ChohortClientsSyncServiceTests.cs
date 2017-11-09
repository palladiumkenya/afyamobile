using System;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Service.Sync;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHTS.Core.Tests.Service.Sync
{
    [TestFixture]
    public class ChohortClientsSyncServiceTests
    {
        private readonly string _local = "http://192.168.1.79:4747";
        private IChohortClientsSyncService _cohortSyncService;
        [SetUp]
        public void SetUp()
        {

            _cohortSyncService =new ChohortClientsSyncService(new RestClient());
        }

        [Test]
        public void should_load_Cohorts()
        {
            var cohorts = _cohortSyncService.GetClients(_local, "26E51B9A-9D69-11E7-ABC4-CEC278B6B50A").Result;

            Assert.IsTrue(cohorts.Count>0);
            foreach (var cohort in cohorts)
            {
                Console.WriteLine(cohort.Client);
            }
        }
    }

    
}