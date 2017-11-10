using System;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Service.Sync;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHTS.Core.Tests.Service.Sync
{
    [TestFixture]
    public class ClientSyncServiceTests
    {
        private readonly string _local = "http://192.168.1.164:4747";
        private IClientSyncService _clientSyncService;

        [SetUp]
        public void SetUp()
        {
            _clientSyncService = new ClientSyncService(new RestClient());
        }
        [Test]
        public void should_search_Cohorts()
        {
            var cohorts = _clientSyncService.SearchClients(_local, "wan").Result;

            Assert.IsTrue(cohorts.Count > 0);
            foreach (var cohort in cohorts)
            {
                Console.WriteLine(cohort.Client);
            }
        }

        [Test]
        public void should_load_Cohorts()
        {
            var cohorts = _clientSyncService.DownloadClient(_local, new Guid("b77f5d59-729f-42fc-bacb-a8260086de8c"))
                .Result;

            Assert.IsNotNull(cohorts);
            Console.WriteLine(cohorts.Client);
        }
    }
}