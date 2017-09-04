using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Infrastructure.Repository.Interview;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;


namespace LiveHTS.Infrastructure.Tests.Repository.Survey
{
    [TestClass]
    public class EncounterRepositoryTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private IEncounterRepository _encounterRepository;
        private Guid _clientId, _practiceId;
        private List<Encounter> _encounters;
        private Guid _encounterTypeId;
        private Guid _formId;

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _encounterRepository = new EncounterRepository(_liveSetting);

            _encounters = TestDataHelpers.Encounters;
            
            _formId = TestDataHelpers._formId;
            _encounterTypeId = TestDataHelpers._encounterTypeId;
            _clientId = TestDataHelpers._clients.First().Id;
        }

        [TestMethod]
        public void should_Load_Encounter_By_Id()
        {
            var encounter = _encounterRepository.Load(_encounters.First().Id);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
            Assert.IsFalse(encounter.Obses.Any());
        }
        [TestMethod]
        public void should_Load_Encounter_With_Obs_By_Id()
        {
            var encounter = _encounterRepository.Load(_encounters.First().Id, true);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
            Assert.IsTrue(encounter.Obses.Any());
        }
        [TestMethod]
        public void should_Load_Encounter_Client()
        {
            var encounter = _encounterRepository.Load(_formId, _encounterTypeId, _clientId);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
            Assert.IsFalse(encounter.Obses.Any());
        }
        [TestMethod]
        public void should_Load_Encounter_With_Obs_Client()
        {
            var encounter = _encounterRepository.Load(_formId, _encounterTypeId, _clientId,true);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
            Assert.IsTrue(encounter.Obses.Any());
        }
        [TestMethod]
        public void should_Load_All_Encounters_By_Client()
        {
            var encounters = _encounterRepository.LoadAll(_formId,  _clientId).ToList();
            Assert.IsTrue(encounters.Any());
            foreach (var encounter in encounters)
            {
                Console.WriteLine(encounter);
            }
        }
    }
}