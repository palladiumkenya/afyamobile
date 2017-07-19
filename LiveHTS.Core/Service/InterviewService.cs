using System;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Service
{
    public class InterviewService:IInterviewService
    {
        private readonly IDirectorService _directorService;
        private  Manifest _manifest;
        private  Question _liveQuestion;

        public Manifest Manifest
        {
            get { return _manifest; }
        }

        public Question LiveQuestion
        {
            get { return _liveQuestion; }
        }

        public InterviewService(IDirectorService directorService)
        {
            _directorService = directorService;
        }


        

        public void Open(Guid formId, Guid encounterTypeId, Guid clientId, Guid practiceId)
        {
            _directorService.RefreshManifest(formId,encounterTypeId,clientId, practiceId);
            _manifest = _directorService.Manifest;
        }

        public void Start(Guid practiceId, Guid deviceId, Guid providerId, Guid userId)
        {
            _directorService.StartEncounter(practiceId,deviceId,providerId,userId );
            _manifest = _directorService.Manifest;
        }

        public void Resume()
        {
            throw new System.NotImplementedException();
        }

        public void Discard()
        {
            throw new System.NotImplementedException();
        }
    }
}