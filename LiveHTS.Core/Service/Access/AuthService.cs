using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Services.Access;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Service.Access
{
    public class AuthService:IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IPracticeRepository _practiceRepository;
        private readonly IDeviceRepository _deviceRepository;
        public AuthService(IUserRepository userRepository, IProviderRepository providerRepository, IPracticeRepository practiceRepository, IDeviceRepository deviceRepository)
        {
            _userRepository = userRepository;
            _providerRepository = providerRepository;
            _practiceRepository = practiceRepository;
            _deviceRepository = deviceRepository;
        }

        public Provider GetDefaultProvider()
        {
            return _providerRepository.GetDefaultProvider();
        }

        public Practice GetDefaultPractice()
        {
            return _practiceRepository.GetDefault();
        }

        public Device GetDefaultDevice()
        {
            return _deviceRepository.GetDefault();
        }

        public User SignIn(string username, string password)
        {
            username = username.ToLower().Trim();

            var user = _userRepository
                .GetAll(x => x.UserName.ToLower() == username)
                .FirstOrDefault();

            if (null == user)
                throw new Exception("wrong User or Password !");

            if (String.CompareOrdinal(password, user.Password) == 0)
                return _userRepository.Get(user.Id);

            throw new Exception("wrong User or Password !");
        }
    }
}