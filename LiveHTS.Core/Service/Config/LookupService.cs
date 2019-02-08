using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Repository.Lookup;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.SharedKernel.Custom;

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
        private readonly IRelationshipTypeRepository _relationshipTypeRepository;
        private readonly IEncounterTypeRepository _encounterTypeRepository;
        private readonly ICategoryRepository _categoryRepository;
        public LookupService(ICountyRepository countyRepository, ISubCountyRepository subCountyRepository, IPracticeRepository practiceRepository, IPracticeTypeRepository practiceTypeRepository, IMaritalStatusRepository maritalStatusRepository, IKeyPopRepository keyPopRepository, IIdentifierTypeRepository identifierTypeRepository, IRelationshipTypeRepository relationshipTypeRepository, IEncounterTypeRepository encounterTypeRepository, ICategoryRepository categoryRepository)
        {
            _countyRepository = countyRepository;
            _subCountyRepository = subCountyRepository;
            _practiceRepository = practiceRepository;
            _practiceTypeRepository = practiceTypeRepository;
            _maritalStatusRepository = maritalStatusRepository;
            _keyPopRepository = keyPopRepository;
            _identifierTypeRepository = identifierTypeRepository;
            _relationshipTypeRepository = relationshipTypeRepository;
            _encounterTypeRepository = encounterTypeRepository;
            _categoryRepository = categoryRepository;
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

        public IEnumerable<Practice> GetPractices()
        {
            return GetPractices(new[] { "Facility" });
        }

        public IEnumerable<Practice> GetPractices(string[] typeIds)
        {
            return _practiceRepository.GetAll(x => typeIds.Contains(x.PracticeTypeId)).ToList();
        }

        public IEnumerable<MaritalStatus> GetMaritalStatuses(bool addSelectOption = false, string selectOption = "[Select Option]")
        {
            var maritalStatuses = new List<MaritalStatus>();
            if (addSelectOption)
            {
                var initialSelected = MaritalStatus.CreateInitial(selectOption);
                maritalStatuses.Add(initialSelected);
            }
            var list = _maritalStatusRepository.GetAll().ToList();
            if (null != list && list.Count > 0)
            {
                maritalStatuses.AddRange(list);
            }
            return maritalStatuses;
        }
        public IEnumerable<KeyPop> GetKeyPops(bool addSelectOption = false, string selectOption = "[Select Option]")
        {
            var keyPops = new List<KeyPop>();
            if (addSelectOption)
            {
                var initialSelected = KeyPop.CreateInitial(selectOption);
                keyPops.Add(initialSelected);
            }
            var list = _keyPopRepository.GetAll().ToList();
            if (null != list && list.Count > 0)
            {
                keyPops.AddRange(list);
            }
            return keyPops;
        }


        public IEnumerable<IdentifierType> GetIdentifierTypes()
        {
            return _identifierTypeRepository.GetAll().ToList();
        }

        public IEnumerable<RelationshipType> GetRelationshipTypes()
        {
            return _relationshipTypeRepository.GetAll().ToList();
        }

        public EncounterType GetDefaultEncounterType(Guid? id)
        {
            if (id.IsNullOrEmpty())
            {
                return _encounterTypeRepository.GetDefault();
            }

            return _encounterTypeRepository.Get(id.Value);
        }

        public IEnumerable<CategoryItem> GetCategoryItems(string code, bool addSelectOption = false, string selectOption = "[Select Option]", bool voided = false)
        {
            var categoryItems = new List<CategoryItem>();

            if (addSelectOption)
            {
                var initialSelected = CategoryItem.CreateInitial(selectOption);
                categoryItems.Add(initialSelected);
            }

            var categotry = _categoryRepository.GetWithCode(code);
            if (null != categotry)
            {
                var items = categotry.Items
                    .Where(x=>x.Voided==voided)
                    .ToList();

                if (null != items && items.Count > 0)
                {
                    categoryItems.AddRange(items);
                    categoryItems = categoryItems.OrderBy(x => x.Rank).ToList();
                }
            }

            return categoryItems;
        }
    }
}