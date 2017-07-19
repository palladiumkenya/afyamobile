using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Repository.Survey;
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

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _encounterRepository = new EncounterRepository(_liveSetting);
            _clientId = TestDataHelpers._clients.First().Id;
            _practiceId = TestDataHelpers._practiceId;
            _encounters = TestDataHelpers.Encounters;
        }

        [TestMethod]
        public void should_Get_Encounter_With_Obs_by_Id()
        {
            var encounter = _encounterRepository.GetWithObs(_encounters.First().Id);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
            Assert.IsTrue(encounter.Obses.ToList().Count>0);

            foreach (var obs in encounter.Obses)
            {
                Assert.AreEqual(obs.EncounterId,encounter.Id);
                Console.WriteLine($"   {obs}");
            }
        }

        [TestMethod]
        public void should_Get_Encounter_With_Obs_by_ClientForm()
        {
            var encounter = _encounterRepository.GetWithObs(_encounters.First().FormId, _encounters.First().ClientId).FirstOrDefault();
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
            Assert.IsTrue(encounter.Obses.ToList().Count > 0);

            foreach (var obs in encounter.Obses)
            {
                Assert.AreEqual(obs.EncounterId, encounter.Id);
                Console.WriteLine($"   {obs}");
            }
        }
        [TestMethod]
        public void should_Get_Active_Encounter()
        {
            var encounter = _encounterRepository.GetActiveEncounter(TestDataHelpers._formId, TestDataHelpers._encounterTypeId, _clientId, _practiceId);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
            Assert.IsTrue(encounter.Obses.ToList().Count > 0);

            foreach (var obs in encounter.Obses)
            {
                Assert.AreEqual(obs.EncounterId, encounter.Id);
                Console.WriteLine($"   {obs}");
            }
        }

        [TestMethod]
        public void should_Get_Active_Encounter_No_Data()
        {
            var encounter = _encounterRepository.GetActiveEncounter(TestDataHelpers._formId, Guid.Empty,  _clientId, _practiceId);
            Assert.IsNull(encounter);
        }
    }
}