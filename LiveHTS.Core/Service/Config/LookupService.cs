using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Service.Config
{
    public class LookupService:ILookupService
    {
        private readonly ICountyRepository _countyRepository;
        private readonly ISubCountyRepository _subCountyRepository;
        private readonly IPracticeRepository _practiceRepository;
        private readonly IPracticeTypeRepository _practiceTypeRepository;

        public LookupService(ICountyRepository countyRepository, ISubCountyRepository subCountyRepository, IPracticeRepository practiceRepository, IPracticeTypeRepository practiceTypeRepository)
        {
            _countyRepository = countyRepository;
            _subCountyRepository = subCountyRepository;
            _practiceRepository = practiceRepository;
            _practiceTypeRepository = practiceTypeRepository;
        }

        public IEnumerable<County> GetCounties()
        {
            return _countyRepository.GetAll().ToList();
        }

        public IEnumerable<SubCounty> GetSubCounties(int[] countyIds)
        {
            return _subCountyRepository.GetAll(x => countyIds.Contains(x.CountyId)).ToList();
        }

        public IEnumerable<PracticeType> GetPracticeTypes()
        {
            return _practiceTypeRepository.GetAll().ToList();
        }

        public IEnumerable<Practice> GetPractices(string[] typeIds)
        {
            return _practiceRepository.GetAll(x => typeIds.Contains(x.PracticeTypeId)).ToList();
        }
    }
}