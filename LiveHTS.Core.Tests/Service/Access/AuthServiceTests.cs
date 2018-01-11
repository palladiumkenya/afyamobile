using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Services.Access;
using LiveHTS.Core.Service.Access;
using LiveHTS.Infrastructure.Repository.Config;
using LiveHTS.Infrastructure.Repository.Subject;
using SQLite;
using NUnit.Framework;

namespace LiveHTS.Core.Tests.Service.Access
{
    [TestFixture]
    public class AuthServiceTests
    {
        private bool setNunit = TestHelpers.UseNunit = true;

        private ILiveSetting _liveSetting;
        private IUserRepository _userRepository;
        private IProviderRepository _providerRepository;
        private IAuthService _authService;
        private SQLiteConnection _database = TestHelpers.GetDatabase();

        [SetUp]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _userRepository = new UserRepository(_liveSetting);
            _providerRepository=new ProviderRepository(_liveSetting);
            _authService=new AuthService(_userRepository,_providerRepository,new PracticeRepository(_liveSetting),new DeviceRepository(_liveSetting) );
        }

        [Test]
        public void should_SignIn()
        {
            var user = _authService.SignIn("admin", "maun2806");
            Assert.IsNotNull(user);
            Console.WriteLine(user);

            Assert.Throws<Exception>(()=>_authService.SignIn("admin", "Maun2806"));
        }
      
        [Test]
        public void should_GetDefaultProvider()
        {
            var user = _authService.GetDefaultProvider();
            Assert.IsNotNull(user);
            Console.WriteLine(user);

            Assert.Throws<Exception>(() => _authService.SignIn("admin", "Maun2806"));
        }
        [Test]
        public void should_GetDefaultPractice()
        {
            var user = _authService.GetDefaultPractice();
            Assert.IsNotNull(user);
            Console.WriteLine(user);

            Assert.Throws<Exception>(() => _authService.SignIn("admin", "Maun2806"));
        }
        [Test]
        public void should_GetDefaultDevice()
        {
            var user = _authService.GetDefaultDevice();
            Assert.IsNotNull(user);
            Console.WriteLine(user);

            Assert.Throws<Exception>(() => _authService.SignIn("admin", "Maun2806"));
        }
    }
}