using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
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

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _encounterRepository = new EncounterRepository(_liveSetting);
        }

        [TestMethod]
        public void should_Get_Active_Encounter()
        {
            var encounter = _encounterRepository.GetActiveEncounter(TestDataHelpers._formId, TestDataHelpers._encounterTypeId, TestDataHelpers._clients.First().Id);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
            Assert.IsTrue(encounter.Obses.ToList().Count>0);

            foreach (var obs in encounter.Obses)
            {
                Assert.AreEqual(obs.EncounterId,encounter.Id);
                Console.WriteLine($"   {obs}");
            }
        }
    }
}