using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Service;
using LiveHTS.Core.Service.Config;
using LiveHTS.Infrastructure.Repository.Config;
using LiveHTS.Infrastructure.Repository.Survey;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Config
{
    [TestFixture]
    public class DeviceSetupServiceTests
    {
        private bool setNunit = TestHelpers.UseNunit = true;

        private ILiveSetting _liveSetting;

        private IDeviceRepository _deviceRepository;

        private IDeviceSetupService _deviceSetupService;
        private SQLiteConnection _database = TestHelpers.GetDatabase();

        [SetUp]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _deviceRepository=new DeviceRepository(_liveSetting);
            _deviceSetupService=new DeviceSetupService(_deviceRepository);
        }

        [Test]
        public void should_GetDefault_Device()
        {
            var device = _deviceSetupService.GetDefault();
            Assert.IsNotNull(device);
            Console.WriteLine(device);
        }
        [Test]
        public void should_Register_Device()
        {
            var device=new Device("xserial","xcode", "xname", Guid.NewGuid());
            _deviceSetupService.Register(device);
            var savedDevice= _deviceSetupService.GetDefault();
            Assert.IsNotNull(savedDevice);
            Assert.AreEqual("xname", savedDevice.Name);
            Console.WriteLine(savedDevice);
        }
    }
}