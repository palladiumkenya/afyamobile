using System;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Service
{
    public class DirectorService:IDirectorService
    {
        private readonly IFormRepository _formRepository;
        private readonly IEncounterRepository _encounterRepository;

        private  Manifest _manifest;

        public Manifest Manifest
        {
            get { return _manifest; }
        }

        public DirectorService(IFormRepository formRepository, IEncounterRepository encounterRepository)
        {
            _formRepository = formRepository;
            _encounterRepository = encounterRepository;
        }

        public void RefreshManifest(Guid formId, Guid encounterTypeId, Guid clientId, Guid practiceId)
        {
            var questions = _formRepository.GetWithQuestions(formId);

            var encounter = _encounterRepository.GetActiveEncounter(formId, encounterTypeId, clientId, practiceId);

            _manifest = Manifest.Create(questions, encounter,formId,encounterTypeId,clientId, practiceId);
        }

        public Encounter StartEncounter(Guid practiceId, Guid deviceId, Guid providerId, Guid userId)
        {
            Encounter encounter = null;

            var existingEncounter = _manifest.GetEncounter();
            if (null != existingEncounter)
            {
                encounter = _encounterRepository.GetWithObs(existingEncounter.Id);
            }
            else
            {
                var newEncounter = Encounter.CreateNew(_manifest.ClientId, _manifest.FormId, _manifest.EncounterTypeId,
                    practiceId, deviceId, providerId, userId);

                _encounterRepository.Save(newEncounter);

                encounter= _encounterRepository.GetWithObs(newEncounter.Id);
            }

            _manifest.UpdateEncounter(encounter);

            return encounter;
        }

        public Question GetLiveQuestion()
        {
            throw new System.NotImplementedException();
        }
    }
}