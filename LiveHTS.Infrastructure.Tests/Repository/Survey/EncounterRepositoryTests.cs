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
        public void should_Get_Encounter_With_Obs_by_Id()
        {
            var encounter = _encounterRepository.Get(_encounters.First().Id);
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
            var encounter = _encounterRepository.GetWithObs(_formId,_encounterTypeId,_clientId).FirstOrDefault();
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
            Assert.IsTrue(encounter.Obses.ToList().Count > 0);

            foreach (var obs in encounter.Obses)
            {
                Assert.AreEqual(obs.EncounterId, encounter.Id);
                Console.WriteLine($"   {obs}");
            }
        }
    }
}