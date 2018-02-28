using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Engine;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Engine;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.Service.Clients;
using LiveHTS.Infrastructure.Repository.Interview;
using LiveHTS.Infrastructure.Repository.Subject;
using LiveHTS.Infrastructure.Repository.Survey;
using LiveHTS.SharedKernel.Model;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Clients
{
    [TestFixture]
    public class ObsServiceTests
    {
        private bool setNunit = TestHelpers.UseNunit = true;

        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        
        private IFormRepository _formRepository;
        private IEncounterRepository _encounterRepository;
        private IObsRepository _obsRepository;
        
        private INavigationEngine _navigationEngine;
        private IValidationEngine _validationEngine;

        private IObsService _obsService;
        private Guid _formId;
        private Form _form;
        private Encounter _encounterNew;
        private Encounter _encounter;
        private ClientStateRepository _clientStateRepository;

        [SetUp]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _formRepository = new FormRepository(_liveSetting,
                new QuestionRepository(_liveSetting,
                    new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting))));
            _encounterRepository=new EncounterRepository(_liveSetting);
            _obsRepository=new ObsRepository(_liveSetting);
            _clientStateRepository = new ClientStateRepository(_liveSetting);
            _formId = TestDataHelpers._formId;
            _form = _formRepository.GetWithQuestions(_formId, true);
            _encounterNew = TestHelpers.CreateTestEncounters(_form);            
            _encounter = TestHelpers.CreateTestEncountersWithObs(_form);
            _navigationEngine = new NavigationEngine();
            _validationEngine=new ValidationEngine();
            _obsService=new ObsService(_formRepository,_encounterRepository,_obsRepository,_navigationEngine,_validationEngine,_clientStateRepository);

        }

        [Test]
        public void should_Initialize()
        {
            _obsService.Initialize(_encounterNew);
            var manifest = _obsService.Manifest;
            Assert.IsNotNull(manifest);
            Assert.IsTrue(manifest.HasQuestions());
            Assert.IsFalse(manifest.HasResponses());
            

            _obsService.Initialize(_encounter);
           var  completedManifest = _obsService.Manifest;
            Assert.IsNotNull(completedManifest);
            Assert.IsTrue(completedManifest.HasQuestions());
            Assert.IsTrue(completedManifest.HasResponses());

            Console.WriteLine(manifest);
            Console.WriteLine(new string('-',30));
            Console.WriteLine(completedManifest);
        }
        
        [Test]
        public void should_GetLiveQuestion_First()
        {
            //Q1.Consent

            _obsService.Initialize(_encounterNew);
            var question = _obsService.GetLiveQuestion();
            Assert.IsNotNull(question);
            Assert.AreEqual(1, question.Rank);
            Console.WriteLine(question);

            Assert.IsFalse(question.SkippedQuestionIds.Any());
            Console.WriteLine($"skipped {question.SkippedQuestionIds.Count}");
        }
        [Test]
        public void should_GetLiveQuestion_Other()
        {
            //  4.Referall   >>  5.Discordant
            _encounterNew.Obses = _encounter.Obses.Take(4).ToList();
            _encounterNew.Obses.First().ValueCoded = TestDataHelpers._consentNo;
            _obsService.Initialize(_encounterNew);


            var question = _obsService.GetLiveQuestion();
            Assert.IsNotNull(question);
            Assert.AreEqual(5, question.Rank);
            Console.WriteLine(question);

            Assert.IsFalse(question.SkippedQuestionIds.Any());
            Console.WriteLine($"skipped {question.SkippedQuestionIds.Count}");
        }

        
        [Test]
        public void should_GetLiveQuestion_Other_Branched()
        {
            //Q1.Consent=N GOTO Q4.Referall

            _encounterNew.Obses = _encounter.Obses.Take(1).ToList();
            _encounterNew.Obses.First().ValueCoded = TestDataHelpers._consentNo;
            _obsService.Initialize(_encounterNew);

            var question = _obsService.GetLiveQuestion();
            Assert.IsNotNull(question);
            Assert.AreEqual(4, question.Rank);
            Console.WriteLine(question);

            Assert.IsTrue(question.SkippedQuestionIds.Any());
            Console.WriteLine($"skipped {question.SkippedQuestionIds.Count}");
        }


        [Test]
        public void should_GetNextQuestion()
        {
            //  4.Referall   >>  5.Discordant

            var currentQuestionId = _form.Questions.First(x => x.Rank == 4).Id;
            _obsService.Initialize(_encounter);


            var question = _obsService.GetNextQuestion(currentQuestionId);
            Assert.IsNotNull(question);
            Assert.AreEqual(5, question.Rank);
            Console.WriteLine(question);

        }
        
        [Test]
        public void should_GetPreviousQuestion()
        {
            //  4.Referall   <<  5.Discordant

            var currentQuestionId = _form.Questions.First(x => x.Rank == 5).Id;
            _obsService.Initialize(_encounter);

            var question = _obsService.GetPreviousQuestion(currentQuestionId);
            Assert.IsNotNull(question);
            Assert.AreEqual(4, question.Rank);
            Console.WriteLine(question);
        }

        [Test]
        public void should_GetQuestion()
        {
            // 5.Discordant

            var currentQuestionId = _form.Questions.First(x => x.Rank == 5).Id;
            _obsService.Initialize(_encounter);

            var question = _obsService.GetQuestion(currentQuestionId, _obsService.Manifest);
            Assert.IsNotNull(question);
            Assert.AreEqual(currentQuestionId, question.Id);
            Assert.AreEqual(5, question.Rank);
            Console.WriteLine(question);
        }


        [Test]
        public void should_SaveResponse()
        {
            // 1.Consent
           _encounterNew = new EncounterService(_encounterRepository,_formRepository).StartEncounter(_encounterNew);
            var currentQuestionId = _form.Questions.First(x => x.Rank == 1).Id;
            _obsService.Initialize(_encounterNew);
            _obsService.SaveResponse(_encounterNew.Id,_encounterNew.ClientId, currentQuestionId,TestDataHelpers._consentYes);

            var manifest = _obsService.Manifest;
            var obs= manifest.ResponseStore.First(x => x.QuestionId == currentQuestionId);
            Assert.IsNotNull(obs);
            Assert.AreEqual(TestDataHelpers._consentYes, obs.Obs.ValueCoded);

            var states =
                _clientStateRepository.GetByClientId(_encounterNew.ClientId, _encounterNew.Id, LiveState.HtsConsented)
                    .ToList();

            Assert.True(states.Count > 0);

            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.HtsConsented));
            foreach (var clientState in states)
            {
                Console.WriteLine($"  {clientState}");
            }
        }
        [Test]
        public void should_SaveResponse_Clear_State()
        {
            // 1.Consent
            _encounterNew = new EncounterService(_encounterRepository, _formRepository).StartEncounter(_encounterNew);
            var currentQuestionId = _form.Questions.First(x => x.Rank == 1).Id;
            _obsService.Initialize(_encounterNew);
            _obsService.SaveResponse(_encounterNew.Id, _encounterNew.ClientId, currentQuestionId, TestDataHelpers._consentNo);

            var manifest = _obsService.Manifest;
            var obs = manifest.ResponseStore.First(x => x.QuestionId == currentQuestionId);
            Assert.IsNotNull(obs);
            Assert.AreEqual(TestDataHelpers._consentNo, obs.Obs.ValueCoded);

            var states =
                _clientStateRepository.GetByClientId(_encounterNew.ClientId, _encounterNew.Id, LiveState.HtsConsented)
                    .ToList();

            Assert.True(states.Count == 0);

        }
    }
}