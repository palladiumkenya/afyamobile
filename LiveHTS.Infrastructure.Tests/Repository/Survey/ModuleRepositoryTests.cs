using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.DummyData;
using LiveHTS.Infrastructure.Repository.Survey;
using LiveHTS.SharedKernel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LiveHTS.Infrastructure.Tests.Repository.Survey
{
    [TestClass]
    public class ModuleRepositoryTests
    {
        private readonly ILiveDatabase _database=new LiveDatabase();
        private IModuleRepository _moduleRepository;

        [TestInitialize]
        public void SetUp()
        {
            _moduleRepository=new ModuleRepository(_database);
        }
        [TestMethod]
        public void should_GetDefaultModule()
        {
            var module = _moduleRepository.GetDefaultModule();
            Assert.IsNotNull(module);
            Assert.IsTrue(module.Forms.ToList().Count>0);
            Console.WriteLine(module.Name);
            foreach (var form in module.Forms)
            {
                Console.WriteLine($" >.{form.Name}");
            }
        }
        
    }
}

