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

namespace LiveHTS.Core.Service.Interview
{
    public class PartnerTracingService : IPartnerTracingService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IObsPartnerTraceResultRepository _obsTraceResultRepository;
        private readonly IClientStateRepository _clientStateRepository;

        private List<CategoryItem> _categoryItems;
        
        public PartnerTracingService(IEncounterRepository encounterRepository, IObsPartnerTraceResultRepository obsTraceResultRepository, IClientStateRepository clientStateRepository)
        {
            _encounterRepository = encounterRepository;
            _obsTraceResultRepository = obsTraceResultRepository;
            _clientStateRepository = clientStateRepository;
        }

        public Encounter OpenEncounter(Guid encounterId)
        {
            var exisitngEncounter = _encounterRepository.LoadTest(encounterId, true);
            return exisitngEncounter;
        }

        public Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId,Guid practiceId, Guid deviceId, Guid indexClientId)
        {
            var exisitngEncounter = _encounterRepository
                .GetAll(x => x.EncounterTypeId == encounterTypeId &&
                             x.ClientId == clientId)
                .FirstOrDefault();

            if (null != exisitngEncounter)
            {
                return OpenEncounter(exisitngEncounter.Id);
            }

            var encounter = Encounter.CreateNew(formId, encounterTypeId, clientId, providerId, userId, practiceId,deviceId,  indexClientId);
            encounter.Started = DateTime.Now;
            _encounterRepository.Save(encounter);
            return encounter;
        }

        public IEnumerable<Encounter> LoadEncounter(Guid clientId, Guid encounterTypeId)
        {

            return _encounterRepository.LoadTestAll(encounterTypeId, clientId, true).ToList();
        }

       

        public void SaveTest(ObsPartnerTraceResult testResult, Guid clientId)
        {
            _obsTraceResultRepository.SaveOrUpdate(testResult);
            _clientStateRepository.SaveOrUpdate(new ClientState(clientId, testResult.EncounterId, ClientState.GetState(testResult.Outcome, "pat")));
        }

        public void DeleteTest(ObsPartnerTraceResult testResult, Guid clientId)
        {
            _obsTraceResultRepository.Delete(testResult.Id);
            _clientStateRepository.DeleteState(clientId, testResult.EncounterId, ClientState.GetState(testResult.Outcome, "pat"));

        }

        public void MarkEncounterCompleted(Guid encounterId, Guid userId, bool completed)
        {
            _encounterRepository.UpdateStatus(encounterId, userId,completed);
        }
    }
}