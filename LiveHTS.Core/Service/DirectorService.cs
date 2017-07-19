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
        private Encounter _encounter;
        private Manifest _manifest;

        public Manifest Manifest
        {
            get { return _manifest; }
        }

        public DirectorService(IFormRepository formRepository, IEncounterRepository encounterRepository, Encounter encounter)
        {
            _formRepository = formRepository;
            _encounterRepository = encounterRepository;
            _encounter = encounter;
        }

        public void Initialize()
        {
            var form= _formRepository.GetWithQuestions(_encounter.FormId);

        }

        public void UpdateManifest()
        {
            
        }

        public Question GetLiveQuestion()
        {
            throw new NotImplementedException();
        }
    }
}