using System;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Repository.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;


namespace LiveHTS.Infrastructure.Tests.Repository.Survey
{
    [TestClass]
    public class ModuleRepositoryTests
    {
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private IModuleRepository _moduleRepository;

        [TestInitialize]
        public void SetUp()
        {
            _moduleRepository=new ModuleRepository(  _database.DatabasePath);
        }

        [TestMethod]
        public void should_GetDefaultModule()
        {
            var module = _moduleRepository.GetDefaultModule();
            Assert.IsNotNull(module);
            Console.WriteLine($"{module.Id}|{module}");
        }

        [TestCleanup]
        public void TEarDown()
        {
            _database.DeleteAll<Module>();
        }
    }
}