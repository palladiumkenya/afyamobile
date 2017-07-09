using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Infrastructure.Repository.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;


namespace LiveHTS.Infrastructure.Tests.Repository.Survey
{
    [TestClass]
    public class ModuleRepositoryTests
    {
        private SQLiteConnection _database = TestHelpers.GetTestDatabase();
        private IModuleRepository _moduleRepository;

        [TestInitialize]
        public void SetUp()
        {
            _moduleRepository=new ModuleRepository(_database);
        }

        [TestMethod]
        public void should_GetDefaultModule()
        {
            var modules = _moduleRepository.GetAll().ToList();
            Assert.IsTrue(modules.Count > 0);

            foreach (var m in modules)
            {
                Console.WriteLine($"{m.Display}");
            }
        }

        public void TEarDown()
        {
            
        }

        
    }
}

