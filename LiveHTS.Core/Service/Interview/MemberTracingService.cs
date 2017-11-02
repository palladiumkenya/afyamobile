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
    public class MemberTracingService : IMemberTracingService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IObsFamilyTraceResultRepository _obsTraceResultRepository;

        private List<CategoryItem> _categoryItems;
        
        public MemberTracingService(IEncounterRepository encounterRepository, IObsFamilyTraceResultRepository obsTraceResultRepository)
        {
            _encounterRepository = encounterRepository;
            _obsTraceResultRepository = obsTraceResultRepository;
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

       

        public void SaveTest(ObsFamilyTraceResult testResult)
        {
            _obsTraceResultRepository.SaveOrUpdate(testResult);            
        }

        public void DeleteTest(ObsFamilyTraceResult testResult)
        {
            _obsTraceResultRepository.Delete(testResult.Id);

        }

       
    }
}