using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Service;
using LiveHTS.Infrastructure.Repository.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Core.Tests.Service
{
    [TestClass]
    public class DirectorServiceTests
    {
        private DirectorService _directorService;
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private Guid _formId, _encounterTypeId, _clientId;

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _directorService = new DirectorService(
                new FormRepository(_liveSetting,
                    new QuestionRepository(_liveSetting,
                        new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting)))),
                new EncounterRepository(_liveSetting));
            _formId = TestDataHelpers._formId;
            _encounterTypeId = TestDataHelpers._encounterTypeId;
            _clientId = TestDataHelpers._clients.First().Id;
        }

        [TestMethod]
        public void should_Refresh_Manifest()
        {
            _directorService.RefreshManifest(_formId,_encounterTypeId,_clientId);
            var manifest=_directorService.Manifest;
            Assert.IsNotNull(manifest);
            Console.WriteLine(manifest);
        }
    }
}