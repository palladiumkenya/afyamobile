using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
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
        private IDirectorService _directorService;
        private IInterviewService _interviewService;
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private Guid _formId, _encounterTypeId, _clientId, _practiceId, _deviceId, _providerId, _userId;
        private Encounter _encounter;

         [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _directorService = new DirectorService(
                new FormRepository(_liveSetting,
                    new QuestionRepository(_liveSetting,
                        new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting)))),
                new EncounterRepository(_liveSetting));
            _interviewService=new InterviewService(_directorService);
            _formId = TestDataHelpers._formId;
            _encounterTypeId = TestDataHelpers._encounterTypeId;
            _clientId = TestDataHelpers._clients.First().Id;
            _practiceId = TestDataHelpers._formId;
            _encounter = TestDataHelpers.Encounters.First();
            _deviceId = TestDataHelpers._deviceId;
            _providerId = TestDataHelpers._providers.First().Id;
            _userId = TestDataHelpers.Users.First().Id;
        }

        [TestMethod]
        public void should_Open_Interview()
        {
            _interviewService.Open(_formId,_encounterTypeId,_clientId,_practiceId);
            var manifest= _interviewService.Manifest;
            Assert.IsNotNull(manifest);
            Console.WriteLine(manifest);
        }


        [TestMethod]
        public void should_Start_Interview()
        {
            _interviewService.Open(_formId, _encounterTypeId, _clientId, _practiceId);
            _interviewService.Start(_practiceId, _deviceId,_providerId,_userId);
            var manifest = _interviewService.Manifest;
            Assert.IsNotNull(manifest);
            Console.WriteLine($"{manifest}");
        }
    }
}