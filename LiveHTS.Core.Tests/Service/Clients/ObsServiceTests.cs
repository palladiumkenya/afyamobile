using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Engine;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Engine;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.Service.Clients;
using LiveHTS.Infrastructure.Repository.Survey;
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
        
        private IDirector _director;
        private IValidator _validator;

        private IObsService _obsService;
        private Guid _formId;
        private Form _form;
        private Encounter _encounterNew;
        private Encounter _encounter;

        [SetUp]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _formRepository = new FormRepository(_liveSetting,
                new QuestionRepository(_liveSetting,
                    new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting))));
            _encounterRepository=new EncounterRepository(_liveSetting);
            _obsRepository=new ObsRepository(_liveSetting);
            _formId = TestDataHelpers._formId;
            _form = _formRepository.GetWithQuestions(_formId, true);
            _encounterNew = TestHelpers.CreateTestEncounters(_form);
            _encounter = TestHelpers.CreateTestEncountersWithObs(_form);
            _director = new SimpleDirector();
            _validator=new SimpleValidator();
            _obsService=new ObsService(_formRepository,_encounterRepository,_obsRepository,_encounterNew,_director,_validator);

        }

        [Test]
        public void should_Initialize()
        {
            _obsService.Initialize();
            var manifest = _obsService.Manifest;
            Assert.IsNotNull(manifest);
            Assert.IsTrue(manifest.HasQuestions());
            Assert.IsFalse(manifest.HasResponses());
            

            _obsService.Initialize(_encounter);
           var  manifestFull = _obsService.Manifest;
            Assert.IsNotNull(manifestFull);
            Assert.IsTrue(manifestFull.HasQuestions());
            Assert.IsTrue(manifestFull.HasResponses());

            Console.WriteLine(manifest);
            Console.WriteLine(new string('-',30));
            Console.WriteLine(manifestFull);
        }

        /*
        [Test]
        public void should_GetLiveQuestion_First_On_NewEncounter()
        {
            //Q1.Consent

            _manifest = Manifest.Create(_form, _encounterNew);

            var question = _director.GetLiveQuestion(_manifest);
            Assert.IsNotNull(question);
            Assert.AreEqual(1, question.Rank);
            Console.WriteLine(question);
        }
        [Test]
        public void should_GetLiveQuestion_Other()
        {
            //  4.Referall   >>  5.Discordant

            _encounterNew.Obses = _encounter.Obses.Take(4).ToList();
            _encounterNew.Obses.First().ValueCoded = TestDataHelpers._consentNo;

            _manifest = Manifest.Create(_form, _encounterNew);

            var question = _director.GetLiveQuestion(_manifest);
            Assert.IsNotNull(question);
            Assert.AreEqual(5, question.Rank);
            Console.WriteLine(question);
        }

        [Test]
        public void should_GetLiveQuestion_Other_Branched()
        {
            //Q1.Consent=N GOTO Q4.Referall

            _encounterNew.Obses = _encounter.Obses.Take(1).ToList();
            _encounterNew.Obses.First().ValueCoded = TestDataHelpers._consentNo;

            _manifest = Manifest.Create(_form, _encounterNew);

            var question = _director.GetLiveQuestion(_manifest);
            Assert.IsNotNull(question);
            Assert.AreEqual(4, question.Rank);
            Console.WriteLine(question);
        }

        [Test]
        public void should_GetNextQuestion()
        {
            //  4.Referall   >>  5.Discordant

            var currentQuestionId = _form.Questions.First(x => x.Rank == 4).Id;
            _manifest = Manifest.Create(_form, _encounter);


            var question = _director.GetNextQuestion(currentQuestionId, _manifest);
            Assert.IsNotNull(question);
            Assert.AreEqual(5, question.Rank);
            Console.WriteLine(question);
        }

        [Test]
        public void should_GetPreviousQuestion()
        {
            //  4.Referall   <<  5.Discordant

            var currentQuestionId = _form.Questions.First(x => x.Rank == 5).Id;
            _manifest = Manifest.Create(_form, _encounter);


            var question = _director.GetPreviousQuestion(currentQuestionId, _manifest);
            Assert.IsNotNull(question);
            Assert.AreEqual(4, question.Rank);
            Console.WriteLine(question);
        }
        */

    }
}