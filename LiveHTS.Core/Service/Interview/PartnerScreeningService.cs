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
    public class PartnerScreeningService : IPartnerScreeningService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IObsPartnerScreeningRepository _obsPartnerScreeningRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IClientStateRepository _clientStateRepository;

        private List<CategoryItem> _categoryItems;

        public PartnerScreeningService(IEncounterRepository encounterRepository, IObsPartnerScreeningRepository obsPartnerScreeningRepository, ICategoryRepository categoryRepository, IClientStateRepository clientStateRepository)
        {
            _encounterRepository = encounterRepository;
            _obsPartnerScreeningRepository = obsPartnerScreeningRepository;
            _categoryRepository = categoryRepository;
            _clientStateRepository = clientStateRepository;
        }

        public Encounter OpenEncounter(Guid encounterId)
        {
            var exisitngEncounter = _encounterRepository.LoadTest(encounterId, true);
            return exisitngEncounter;
        }

        public Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId, Guid practiceId, Guid deviceId, Guid indexClientId)
        {
            var exisitngEncounter = _encounterRepository
                .GetAll(x => x.EncounterTypeId == encounterTypeId &&
                             x.ClientId == clientId)
                .FirstOrDefault();

            if (null != exisitngEncounter)
            {
                return OpenEncounter(exisitngEncounter.Id);
            }

            var encounter = Encounter.CreateNew(formId, encounterTypeId, clientId, providerId, userId, practiceId, deviceId,indexClientId);
            encounter.Started = DateTime.Now;
            _encounterRepository.Save(encounter);
            return encounter;
        }

        public IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId)
        {

            return _encounterRepository.LoadTestAll(encounterTypeId, clientId, true).ToList();
        }

        public void SavePartnerScreening(ObsPartnerScreening testResult, Guid clientId)
        {
            _obsPartnerScreeningRepository.SaveOrUpdate(testResult);

            _clientStateRepository.SaveOrUpdate(new ClientState(clientId, testResult.EncounterId, LiveState.PartnerScreened));

            _clientStateRepository.DeleteState(clientId, testResult.EncounterId, LiveState.PartnerEligibileNo);
            _clientStateRepository.DeleteState(clientId, testResult.EncounterId, LiveState.PartnerEligibileYes);
            if (testResult.Eligibility == new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"))
            {
                _clientStateRepository.SaveOrUpdate(new ClientState(clientId, testResult.EncounterId, LiveState.PartnerEligibileYes));
            }
            else
            {
                _clientStateRepository.SaveOrUpdate(new ClientState(clientId, testResult.EncounterId, LiveState.PartnerEligibileNo));
            }
        }

        public void MarkEncounterCompleted(Guid encounterId, Guid userId, bool completed)
        {
            _encounterRepository.UpdateStatus(encounterId,userId, completed);
        }
    }
}