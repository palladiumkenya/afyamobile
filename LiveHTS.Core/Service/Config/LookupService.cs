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
        private readonly IMaritalStatusRepository _maritalStatusRepository;
        private readonly IKeyPopRepository _keyPopRepository;
        private readonly IIdentifierTypeRepository _identifierTypeRepository;

        public LookupService(ICountyRepository countyRepository, ISubCountyRepository subCountyRepository, IPracticeRepository practiceRepository, IPracticeTypeRepository practiceTypeRepository, IMaritalStatusRepository maritalStatusRepository, IKeyPopRepository keyPopRepository, IIdentifierTypeRepository identifierTypeRepository)
        {
            _countyRepository = countyRepository;
            _subCountyRepository = subCountyRepository;
            _practiceRepository = practiceRepository;
            _practiceTypeRepository = practiceTypeRepository;
            _maritalStatusRepository = maritalStatusRepository;
            _keyPopRepository = keyPopRepository;
            _identifierTypeRepository = identifierTypeRepository;
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

        public Practice GetDefault()
        {
            return _practiceRepository.GetDefault();
        }

        public IEnumerable<Practice> GetDefaultPractices()
        {
            return _practiceRepository.GetAll(x =>x.IsDefault).ToList();
        }

        public IEnumerable<Practice> GetPractices(string[] typeIds)
        {
            return _practiceRepository.GetAll(x => typeIds.Contains(x.PracticeTypeId)).ToList();
        }

        public IEnumerable<MaritalStatus> GetMaritalStatuses()
        {
            return _maritalStatusRepository.GetAll().ToList(); 
        }

        public IEnumerable<KeyPop> GetKeyPops()
        {
            return _keyPopRepository.GetAll().ToList();
        }

        public IEnumerable<IdentifierType> GetIdentifierTypes()
        {
            return _identifierTypeRepository.GetAll().ToList();
        }
    }
}