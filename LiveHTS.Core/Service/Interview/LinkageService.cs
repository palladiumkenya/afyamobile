using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Lookup;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Interview
{
    public class LinkageService : ILinkageService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IObsTraceResultRepository _obsTraceResultRepository;
        private readonly IObsLinkageRepository _obsLinkageRepository;
        private readonly ICategoryRepository _categoryRepository;

        private List<CategoryItem> _categoryItems;
        
        public LinkageService(IEncounterRepository encounterRepository,
            IObsTraceResultRepository obsTraceResultRepository,IObsLinkageRepository obsLinkageRepository, ICategoryRepository categoryRepository)
        {
            _encounterRepository = encounterRepository;
            _obsTraceResultRepository = obsTraceResultRepository;
            _obsLinkageRepository = obsLinkageRepository;
            _categoryRepository = categoryRepository;
            LoadItems();
        }

        public Encounter OpenEncounter(Guid encounterId)
        {
            var exisitngEncounter = _encounterRepository.LoadTest(encounterId, true);
            return exisitngEncounter;
        }

        public Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId,Guid practiceId, Guid deviceId)
        {
            var exisitngEncounter = _encounterRepository
                .GetAll(x => x.EncounterTypeId == encounterTypeId &&
                             x.ClientId == clientId)
                .FirstOrDefault();

            if (null != exisitngEncounter)
            {
                return OpenEncounter(exisitngEncounter.Id);
            }

            var encounter = Encounter.CreateNew(formId, encounterTypeId, clientId, providerId, userId, practiceId,deviceId);
            encounter.Started = DateTime.Now;
            _encounterRepository.Save(encounter);
            return encounter;
        }

        public IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId)
        {

            return _encounterRepository.LoadTestAll(encounterTypeId, clientId, true).ToList();
        }

        public void SaveLinkage(ObsLinkage testResult)
        {
            _obsLinkageRepository.SaveOrUpdate(testResult);
        }

        public void SaveTest(ObsTraceResult testResult)
        {
            _obsTraceResultRepository.SaveOrUpdate(testResult);            
        }

        public void DeleteTest(ObsTraceResult testResult)
        {
            _obsTraceResultRepository.Delete(testResult.Id);

           
        }

        public void MarkEncounterCompleted(Guid encounterId, Guid userId, bool completed)
        {
            _encounterRepository.UpdateStatus(encounterId,userId, completed);
        }

        private void LoadItems()
        {
            if (null != _categoryItems && _categoryItems.Count > 0)
                return;

            _categoryItems = new List<CategoryItem>();

            var categotry = _categoryRepository.GetWithCode("TestResult");

            if (null != categotry)
            {
                var items = categotry.Items.ToList();
                if (null != items && items.Count > 0)
                {
                    _categoryItems.AddRange(items);
                }
            }

            var categotry2 = _categoryRepository.GetWithCode("FinalResult");

            if (null != categotry2)
            {
                var items = categotry2.Items.ToList();
                if (null != items && items.Count > 0)
                {
                    _categoryItems.AddRange(items);
                }
            }
        
        }
    }
}