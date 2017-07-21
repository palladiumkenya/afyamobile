using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using LiveHTS.Core.Engine;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Engine;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Repository.Survey;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Engine
{
    [TestFixture]
    public class NavigationEngineTests
    {
        private bool setNunit = TestHelpers.UseNunit = true;

        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();

        private Form _form;
        private Guid _formId;
        private Encounter _encounter, _encounterNew;
        private Response _responseRequired;
        private Manifest _manifest;
        private INavigationEngine _navigationEngine;

        [SetUp]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            var formRepository = new FormRepository(_liveSetting,
                new QuestionRepository(_liveSetting,
                    new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting))));
            _formId = TestDataHelpers._formId;
            _form = formRepository.GetWithQuestions(_formId, true);
            _encounter = TestHelpers.CreateTestEncountersWithObs(_form);
            _encounterNew = TestHelpers.CreateTestEncounters(_form);
            _navigationEngine=new NavigationEngine();
        }

        [Test]
        public void should_GetLiveQuestion_First_On_NewEncounter()
        {
            //Q1.Consent

            _manifest = Manifest.Create(_form, _encounterNew);

            var question = _navigationEngine.GetLiveQuestion(_manifest);
            Assert.IsNotNull(question);
            Assert.AreEqual(1,question.Rank);
            Console.WriteLine(question);
        }
        [Test]
        public void should_GetLiveQuestion_Other()
        {
            //  4.Referall   >>  5.Discordant

            _encounterNew.Obses = _encounter.Obses.Take(4).ToList();
            _encounterNew.Obses.First().ValueCoded = TestDataHelpers._consentNo;

            _manifest = Manifest.Create(_form, _encounterNew);

            var question = _navigationEngine.GetLiveQuestion(_manifest);
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

            var question = _navigationEngine.GetLiveQuestion(_manifest);
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
                       
            var question = _navigationEngine.GetNextQuestion(currentQuestionId,_manifest);
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

            var question = _navigationEngine.GetPreviousQuestion(currentQuestionId, _manifest);
            Assert.IsNotNull(question);
            Assert.AreEqual(4, question.Rank);
            Console.WriteLine(question);
        }

    }
}