using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Service.Clients;
using LiveHTS.Infrastructure.Repository.Interview;
using LiveHTS.Infrastructure.Repository.Survey;
using LiveHTS.SharedKernel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Clients
{
    [TestClass]
    public class EncounterServiceTests
    {
        private ILiveSetting _liveSetting;
        private IEncounterRepository _encounterRepository;
        private IFormRepository _formRepository;
        private IEncounterService _encounterService;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private Guid _formId, _encounterTypeId, _clientId, _providerId, _userId, _practiceId;
        private List<Encounter> _encounters;

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _encounterRepository = new EncounterRepository(_liveSetting);
            _formRepository=new FormRepository(_liveSetting,new QuestionRepository(_liveSetting,new ConceptRepository(_liveSetting,new CategoryRepository(_liveSetting))));
            _encounterService = new EncounterService(_encounterRepository,_formRepository);

            _formId = TestDataHelpers._formId;
            _encounterTypeId = TestDataHelpers._encounterTypeId;
            _clientId = TestDataHelpers._clients.First().Id;
            _providerId = TestDataHelpers._providers.First().Id;
            _userId = TestDataHelpers.Users.First().Id;
            _encounters = TestDataHelpers.Encounters;
        }
        [TestMethod]
        public void should_Start_Encounter_New()
        {
            //Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId,Guid practiceId, Guid deviceId)
            var encounter = _encounterService.StartEncounter(_formId, _encounterTypeId, Guid.NewGuid(), _providerId, _userId, _practiceId,Guid.NewGuid(),null,VisitType.Initial);
            Assert.IsNotNull(encounter);
            Assert.IsFalse(encounter.HasObs);
            Assert.IsFalse(encounter.IsComplete);
            Console.WriteLine(encounter);
        }
        [TestMethod]
        public void should_Start_Encounter_Exisitng()
        {
            var encounter = _encounterService.StartEncounter(_formId, _encounterTypeId, _clientId, _providerId, _userId, _practiceId,Guid.NewGuid(),null, VisitType.Initial);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
        }
        [TestMethod]
        public void should_Open_Encounter_Exisitng()
        {
            var encounter = _encounterService.OpenEncounter(_encounters.First().Id);
            Assert.IsNotNull(encounter);
            Console.WriteLine(encounter);
        }
        [TestMethod]
        public void should_Dicard_Encounter()
        {
            var id = _encounters.First().Id;
             _encounterService.DiscardEncounter(id);

            var encounter = _encounterRepository.Load(id);
            Assert.IsNull(encounter);
        }
    }
}