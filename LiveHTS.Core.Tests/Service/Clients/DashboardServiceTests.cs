using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Service.Clients;
using LiveHTS.Infrastructure.Migrations;
using LiveHTS.Infrastructure.Repository.Interview;
using LiveHTS.Infrastructure.Repository.Subject;
using LiveHTS.Infrastructure.Repository.Survey;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Clients
{
    [TestFixture]
    public class DashboardServiceTests
    {
        private readonly string dbname = "livehts.db";
        private SQLiteConnection _connection;
        private ILiveSetting _liveSetting;
        private IDashboardService _dashboardService;
        

        [SetUp]
        public void SetUp()
        {
            _connection = new SQLiteConnection(dbname);
            _liveSetting = new LiveSetting(_connection.DatabasePath);
            Seeder.Seed(_connection);

            _dashboardService=new DashboardService(new ClientRepository(_liveSetting),new ClientRelationshipRepository(_liveSetting),new ModuleRepository(_liveSetting) ,new EncounterRepository(_liveSetting),new ClientStateRepository(_liveSetting) );
        }

        [Test]
        public void should_LoadModule_With_Form_Program()
        {
            var module = _dashboardService.LoadModule();

            Assert.IsNotNull(module);
            Assert.IsTrue(module.Forms.Count>0);
            foreach (var form in module.Forms)
            {
                Console.WriteLine(form.Display);
                foreach (var program in form.Programs)
                {
                    Console.WriteLine($"  >.{program.Display}");
                }

            }

            
        }
    }
}