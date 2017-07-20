using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Service.Clients;
using LiveHTS.Infrastructure.Repository.Subject;
using LiveHTS.Infrastructure.Repository.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Clients
{
    [TestClass]
    public class DashboardServiceTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private IDashboardService _dashboardService;
        private IClientRepository _clientRepository;
        private IFormRepository _formRepository;      
        private Guid _clientId, _moduleId;
       

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _clientRepository =new ClientRepository(_liveSetting);
            _formRepository=new FormRepository(_liveSetting);
            _dashboardService=new DashboardService(_clientRepository,_formRepository);
            _clientId = TestDataHelpers._clients.First().Id;
            _moduleId = TestDataHelpers._moduleId;
        }
        [TestMethod]
        public void should_Load_Client()
        {
            var cient = _dashboardService.LoadClient(_clientId);
            Assert.IsNotNull(cient);
            Console.WriteLine(cient);
        }
        [TestMethod]
        public void should_Load_Forms()
        {
            var forms = _dashboardService.LoadForms().ToList();
            Assert.IsTrue(forms.Count>0);

            foreach (var form in forms)
            {
                Console.WriteLine(form);
            }
        }
        [TestMethod]
        public void should_Load_Module_Forms()
        {
            var forms = _dashboardService.LoadForms(_moduleId).ToList();
            Assert.IsTrue(forms.Count > 0);

            foreach (var form in forms)
            {
                Console.WriteLine(form);
            }
        }
    }
}