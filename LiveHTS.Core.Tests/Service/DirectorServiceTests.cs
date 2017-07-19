using System;
using System.Collections.Generic;
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
        private Encounter _encounter, _encounterNoObs;
        private IFormRepository _formRepository;
        private IEncounterRepository _encounterRepository;

         [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _formRepository=new FormRepository(_liveSetting,new QuestionRepository(_liveSetting,new ConceptRepository(_liveSetting,new CategoryRepository(_liveSetting))));
            _encounterRepository=new EncounterRepository(_liveSetting);
            _encounter = TestDataHelpers.Encounters.First();
            _encounterNoObs = _encounter;
            _encounterNoObs.Obses = new List<Obs>();
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
            _directorService = new DirectorService(_formRepository, _encounterRepository, _encounterNoObs);
            _directorService.Initialize();
            var manifest = _directorService.Manifest;
            Assert.IsNotNull(manifest);
            Assert.IsFalse(manifest.HasResponses());

            _directorService.UpdateManifest();
            manifest = _directorService.Manifest;
            Assert.IsTrue(manifest.HasResponses());
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