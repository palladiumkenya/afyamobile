using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Service;
using LiveHTS.Infrastructure.Repository.Config;
using LiveHTS.Infrastructure.Repository.Survey;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Service
{
    [TestFixture]
    public class AppDashboardServiceTests
    {
        private bool setNunit = TestHelpers.UseNunit = true;

        private ILiveSetting _liveSetting;

        private IModuleRepository _moduleRepository;
        private IDeviceRepository _deviceRepository;

        private IAppDashboardService _appDashboardService;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _moduleRepository = new ModuleRepository(_liveSetting);
            _deviceRepository = new DeviceRepository(_liveSetting);
            _appDashboardService=new AppDashboardService(_moduleRepository,_deviceRepository);
            _user = TestDataHelpers.Users.First();
        }

        [Test]
        public void should_Load_AppDashboard()
        {

            var appDashboard = _appDashboardService.Load(_user);
            Assert.IsNotNull(appDashboard);
            Assert.IsNotNull(appDashboard.SignedInUser);
            Assert.IsNotNull(appDashboard.Device);
            Assert.IsNotNull(appDashboard.DefaultModule);

            Console.WriteLine(appDashboard);
        }
    }
}