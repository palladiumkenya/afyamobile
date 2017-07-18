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

        public void RefreshManifest(Guid formId, Guid encounterTypeId, Guid clientId)
        {
            var questions = _formRepository.GetWithQuestions(formId);

            var obs = _encounterRepository.GetActiveEncounter(formId, encounterTypeId, clientId);

            _manifest = Manifest.Create(questions, obs);
        }

        public Question GetLiveQuestion()
        {
            throw new System.NotImplementedException();
        }
    }
}