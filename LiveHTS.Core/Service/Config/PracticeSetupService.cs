using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Service.Config
{
    public class PracticeSetupService: IPracticeSetupService
    {
        private readonly IPracticeRepository _practiceRepository;

        public PracticeSetupService(IPracticeRepository practiceRepository)
        {
            _practiceRepository = practiceRepository;
        }

        public Practice GetDefault()
        {
            return _practiceRepository.GetDefault();
        }

        public List<Practice> GetAll()
        {
            return _practiceRepository.GetAll().ToList();
        }

        public void Save(Practice practice)
        {
            var defaultPs = _practiceRepository.GetAll(x => x.IsDefault).ToList();
            foreach (var defaultP in defaultPs)
            {
                defaultP.IsDefault = false;
                _practiceRepository.Update(defaultP);
            }
            practice.IsDefault = true;
            _practiceRepository.InsertOrUpdate(practice);
        }
    }
}