using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Service;
using LiveHTS.Infrastructure.Repository.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Core.Tests.Service
{
    [TestClass]
    public class InterviewServiceTests
    {
        private ILiveSetting _liveSetting;
        private IEncounterRepository _encounterRepository;
        private IInterviewService _interviewService;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private Guid _formId, _encounterTypeId, _clientId,_providerId,_userId;
        private List<Encounter> _encounters;

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _encounterRepository = new EncounterRepository(_liveSetting);

            _interviewService = new InterviewService(_encounterRepository);

            _formId = TestDataHelpers._formId;
            _encounterTypeId = TestDataHelpers._encounterTypeId;
            _clientId = TestDataHelpers._clients.First().Id;
            _providerId = TestDataHelpers._providers.First().Id;
            _userId = TestDataHelpers.Users.First().Id;
            _encounters = TestDataHelpers.Encounters;
        }

        [TestMethod]
        public void should_Load_Encounter()
        {
            var encounterNew = _interviewService.LoadEncounter(_formId, _encounterTypeId, Guid.NewGuid());
            Assert.IsNull(encounterNew);

            var encounter = _interviewService.LoadEncounter(_formId, _encounterTypeId, _clientId);
            Assert.IsNotNull(encounter);
            Assert.IsTrue(encounter.HasObs);
            Assert.IsFalse(encounter.IsComplete);
            Console.WriteLine(encounter);
        }

        [TestMethod]
        public void should_Start_Encounter_New()
        {
            var encounter = _interviewService.StartEncounter(_formId, _encounterTypeId, Guid.NewGuid(), _providerId,_userId);
            Assert.IsNotNull(encounter);
            Assert.IsFalse(encounter.HasObs);
            Assert.IsFalse(encounter.IsComplete);
            Console.WriteLine(encounter);
        }
        [TestMethod]
        public void should_Start_Encounter_Exisitng()
        {
            var encounter = _interviewService.StartEncounter(_formId, _encounterTypeId, _clientId, _providerId, _userId);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
        }
        [TestMethod]
        public void should_Open_Encounter_Exisitng()
        {
            var encounter = _interviewService.OpenEncounter(_encounters.First().Id);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
        }
    }
}