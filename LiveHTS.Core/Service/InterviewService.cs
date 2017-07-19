using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Service
{
    public class InterviewService:IInterviewService
    {
        private readonly IEncounterRepository _encounterRepository;

        public InterviewService(IEncounterRepository encounterRepository)
        {
            _encounterRepository = encounterRepository;
        }

        public Encounter LoadEncounter(Guid formId, Guid encounterTypeId, Guid clientId)
        {
            var encounter=_encounterRepository.GetWithObs(formId, encounterTypeId, clientId).FirstOrDefault();

            return encounter;
        }

        public Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId)
        {
            var exisitngEncounter =_encounterRepository.GetAll(x => x.FormId == formId &&
                                                 x.EncounterTypeId == encounterTypeId &&
                                                 x.ClientId == clientId)
                    .FirstOrDefault();

            if (null != exisitngEncounter)
            {
                exisitngEncounter.Status = "Opened";
                return exisitngEncounter;
            }

            var encounter = Encounter.CreateNew(formId, encounterTypeId, clientId, providerId, userId);
            _encounterRepository.Save(encounter);
            return encounter;
        }

        public Encounter OpenEncounter(Guid encounterId)
        {
            var encounter = _encounterRepository.Get(encounterId);

            return encounter;
        }
    }
}