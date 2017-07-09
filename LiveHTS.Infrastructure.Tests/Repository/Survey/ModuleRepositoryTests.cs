using System;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
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
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private IModuleRepository _moduleRepository;

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _moduleRepository =new ModuleRepository(_liveSetting);
        }

        [TestMethod]
        public void should_GetDefaultModule()
        {
            var module = _moduleRepository.GetDefaultModule();
            Assert.IsNotNull(module);
            Console.WriteLine($"{module.Id}|{module}");
            foreach (var form in module.Forms)
            {
                Assert.IsNotNull(form);
                Assert.AreEqual(module.Id, form.ModuleId);
                Console.WriteLine($" > {form}");
            }
        }

        
    }
}