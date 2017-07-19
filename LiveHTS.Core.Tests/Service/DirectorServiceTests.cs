using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Service;
using LiveHTS.Infrastructure.Repository.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Core.Tests.Service
{
    [TestClass]
    public class DirectorServiceTests
    {
        private DirectorService _directorService;
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private Encounter _encounter;
        private IFormRepository _formRepository;
        private IEncounterRepository _encounterRepository;

         [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _encounter = TestDataHelpers.Encounters.First();
            _directorService = new DirectorService(_formRepository,_encounterRepository,_encounter);
        }
        [TestMethod]
        public void should_Initialize()
        {
            _directorService.Initialize();
            var manifest = _directorService.Manifest;
            Assert.IsNotNull(manifest);
            Console.WriteLine(manifest);
        }

        [TestMethod]
        public void should_Refresh_Manifest()
        {
            _directorService.UpdateManifest();
            var manifest=_directorService.Manifest;
            Assert.IsNotNull(manifest);
            Console.WriteLine(manifest);
        }

        [TestMethod]
        public void should_Get_LiveQuestion()
        {
            var q = _directorService.GetLiveQuestion();
            Assert.IsNotNull(q);
            Console.WriteLine($"Active:{q}");
        }
    }
}