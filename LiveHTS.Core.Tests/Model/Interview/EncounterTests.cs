using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.Service.Clients;
using LiveHTS.Infrastructure.Repository.Interview;
using LiveHTS.Infrastructure.Repository.Survey;
using NUnit.Framework;
using SQLite;


namespace LiveHTS.Core.Tests.Model.Interview
{
    [TestFixture]
    public class EncounterTests
    {
        private bool setNunit = TestHelpers.UseNunit = true;

        private ILiveSetting _liveSetting;
        private IEncounterRepository _encounterRepository;
        private IFormRepository _formRepository;
        private IEncounterService _encounterService;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private Guid _formId, _encounterTypeId, _clientId, _providerId, _userId;
        private List<Encounter> _encounters;


        [SetUp]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _encounterRepository = new EncounterRepository(_liveSetting);
            _formRepository = new FormRepository(_liveSetting, new QuestionRepository(_liveSetting, new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting))));
            _encounterService = new EncounterService(_encounterRepository, _formRepository);

            _formId = TestDataHelpers._formId;
            _encounterTypeId = TestDataHelpers._encounterTypeId;
            _clientId = TestDataHelpers._clients.First().Id;
            _providerId = TestDataHelpers._providers.First().Id;
            _userId = TestDataHelpers.Users.First().Id;
            _encounters = TestDataHelpers.Encounters;
        }

        [Test]
        public void should_Add_Encounter_Obs()
        {
            var encounter = _encounters.First();
            var obsCount = encounter.Obses.ToList().Count;
            
            var concept = Builder<Concept>.CreateNew().Build();
            var question = Builder<Question>.CreateNew()
                .With(x => x.Concept = concept)
                .With(x => x.ConceptId = concept.Id)
                .Build();
            var obs = Builder<Obs>.CreateNew()
                .With(x => x.QuestionId = question.Id)
                .Build();

            obs.ValueText = "Maun";

            encounter.AddOrUpdate(obs);

            var newObs = encounter.Obses.FirstOrDefault(x => x.Id == obs.Id);
            Assert.IsNotNull(newObs);
            Assert.AreEqual(newObs.EncounterId,encounter.Id);
            Assert.IsTrue(encounter.Obses.ToList().Count == obsCount + 1);
            Console.WriteLine(newObs);
        }

        [Test]
        public void should_Not_Add_Encounter_Obs_If_Null()
        {
            var encounter = _encounters.First();
            var obsCount = encounter.Obses.ToList().Count;

            var concept = Builder<Concept>.CreateNew().Build();
            var question = Builder<Question>.CreateNew()
                .With(x => x.Concept = concept)
                .With(x => x.ConceptId = concept.Id)
                .Build();
            var obs = Builder<Obs>.CreateNew()
                .With(x => x.QuestionId = question.Id)
                .Build();

            obs = Obs.Create(obs.QuestionId, obs.EncounterId,_clientId, "Text", "");
            encounter.AddOrUpdate(obs,false);

            var newObs = encounter.Obses.FirstOrDefault(x => x.Id == obs.Id);
            Assert.IsNull(newObs);
            Assert.IsTrue(encounter.Obses.ToList().Count == obsCount);
            
        }
        [Test]
        public void should_Update_Encounter_Obs()
        {
            var encounter = _encounters.First();
            var obsCount = encounter.Obses.ToList().Count;
            var obs = encounter.Obses.Last();
            obs.ValueText = "Maun";

            encounter.AddOrUpdate(obs);

            var updatedObs = encounter.Obses.FirstOrDefault(x => x.Id == obs.Id);
            Assert.IsNotNull(updatedObs);
            Assert.AreEqual("Maun", updatedObs.ValueText);
            Assert.AreEqual(updatedObs.EncounterId, encounter.Id);
            Assert.IsTrue(encounter.Obses.ToList().Count == obsCount);
            Console.WriteLine(updatedObs);
        }
        [Test]
        public void should_Not_Update_Encounter_Obs_if_null()
        {
            var encounter = _encounters.First();
            var obsCount = encounter.Obses.ToList().Count;
            var obs = encounter.Obses.Last();
            

            var newObs = Obs.Create(obs.QuestionId, obs.EncounterId,_clientId, "Single", null);
            newObs.Id = obs.Id;
            

            encounter.AddOrUpdate(newObs, false);

            var updatedObs = encounter.Obses.FirstOrDefault(x => x.Id == newObs.Id);
            Assert.IsNull(updatedObs);
            
        }

    }
}