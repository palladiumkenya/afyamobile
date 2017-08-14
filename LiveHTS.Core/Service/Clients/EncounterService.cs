using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Service.Clients
{
    public class EncounterService:IEncounterService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IFormRepository _formRepository;

        public EncounterService(IEncounterRepository encounterRepository, IFormRepository formRepository)
        {
            _encounterRepository = encounterRepository;
            _formRepository = formRepository;
        }

        public Form LoadForm(Guid formId)
        {
            return _formRepository.GetWithQuestions(formId);
        }

        public Encounter LoadEncounter(Guid formId, Guid encounterTypeId, Guid clientId, bool includeObs = false)
        {
            return _encounterRepository.Load(formId, encounterTypeId, clientId,includeObs);
        }

        public IEnumerable<Encounter> LoadEncounters(Guid formId, Guid clientId, bool includeObs = false)
        {
            return _encounterRepository.LoadAll(formId,  clientId, includeObs);
        }

        public Encounter StartEncounter(Encounter encounter)
        {
            return StartEncounter(encounter.FormId, encounter.EncounterTypeId, encounter.ClientId, encounter.ProviderId,
                encounter.UserId);
        }

        public Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId)
        {
            var exisitngEncounter =_encounterRepository.GetAll(x => x.FormId == formId &&
                                                 x.EncounterTypeId == encounterTypeId &&
                                                 x.ClientId == clientId)
                    .FirstOrDefault();

            if (null != exisitngEncounter)
            {
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

        public void DiscardEncounter(Guid encounterId)
        {
            _encounterRepository.Delete(encounterId);
        }
    }
}