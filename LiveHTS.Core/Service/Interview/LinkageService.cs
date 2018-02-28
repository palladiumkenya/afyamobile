using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Lookup;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Service.Interview
{
    public class LinkageService : ILinkageService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IObsTraceResultRepository _obsTraceResultRepository;
        private readonly IObsLinkageRepository _obsLinkageRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IClientStateRepository _clientStateRepository;

        private List<CategoryItem> _categoryItems;
        
        public LinkageService(IEncounterRepository encounterRepository,
            IObsTraceResultRepository obsTraceResultRepository,IObsLinkageRepository obsLinkageRepository, ICategoryRepository categoryRepository, IClientStateRepository clientStateRepository)
        {
            _encounterRepository = encounterRepository;
            _obsTraceResultRepository = obsTraceResultRepository;
            _obsLinkageRepository = obsLinkageRepository;
            _categoryRepository = categoryRepository;
            _clientStateRepository = clientStateRepository;
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

            var encounter = Encounter.CreateNew(formId, encounterTypeId, clientId, providerId, userId, practiceId,deviceId,null);
            encounter.Started = DateTime.Now;
            _encounterRepository.Save(encounter);
            return encounter;
        }

        public IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId)
        {

            return _encounterRepository.LoadTestAll(encounterTypeId, clientId, true).ToList();
        }

        public void SaveLinkage(ObsLinkage testResult, Guid clientId, bool referral = true)
        {
            _obsLinkageRepository.SaveOrUpdate(testResult);
            if (referral)
            {
                _clientStateRepository.SaveOrUpdate(new ClientState(clientId, testResult.EncounterId, LiveState.HtsReferred));
            }
            else
            {
                _clientStateRepository.SaveOrUpdate(new ClientState(clientId, testResult.EncounterId, LiveState.HtsLinkedCare));
                if (!string.IsNullOrWhiteSpace(testResult.EnrollmentId))
                {
                    _clientStateRepository.SaveOrUpdate(new ClientState(clientId, testResult.EncounterId, LiveState.HtsLinkedEnrolled));
                }
            }

        }

        public void SaveTest(ObsTraceResult testResult, Guid clientId)
        {
            _obsTraceResultRepository.SaveOrUpdate(testResult);

            _clientStateRepository.SaveOrUpdate(new ClientState(clientId, testResult.EncounterId, ClientState.GetState(testResult.Outcome)));
        }

        public void DeleteTest(ObsTraceResult testResult, Guid clientId)
        {
            _obsTraceResultRepository.Delete(testResult.Id);
            _clientStateRepository.DeleteState(clientId, testResult.EncounterId,ClientState.GetState(testResult.Outcome));
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