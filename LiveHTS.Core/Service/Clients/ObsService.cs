using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Engine;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Service.Clients
{
    public class ObsService:IObsService
    {
        private Manifest _manifest;
        private readonly Response _response;
        private Encounter _encounter;
        private readonly INavigationEngine _navigationEngine;
        private readonly IValidationEngine _validationEngine;

        private readonly IFormRepository _formRepository;
        private readonly IEncounterRepository _encounterRepository;
        private readonly IObsRepository _obsRepository;
        

        public Manifest Manifest
        {
            get { return _manifest; }
        }
        public Response Response
        {
            get { return _response; }
        }

        public ObsService(IFormRepository formRepository, IEncounterRepository encounterRepository, IObsRepository obsRepository
            , INavigationEngine navigationEngine,IValidationEngine validationEngine)
        {
            _formRepository = formRepository;
            _encounterRepository = encounterRepository;
            _obsRepository = obsRepository;
            _navigationEngine = navigationEngine;
            _validationEngine = validationEngine;
        }

        public void Initialize(Encounter encounter)
        {
            var currentEncounter = _encounter = encounter;
            
            var form = _formRepository.GetWithQuestions(currentEncounter.FormId, true);
            _manifest = Manifest.Create(form, currentEncounter);
        }

        public List<SetResponse> GetTransformationActions(Manifest currentManifest, Guid currentQuestionId)
        {
            return _navigationEngine.GetActions(currentManifest, currentQuestionId);
        }

        public Question GetLiveQuestion(Manifest manifest = null)
        {
            _manifest = manifest ?? _manifest;
            return _navigationEngine.GetLiveQuestion(_manifest);
        }

        public Question GetLiveQuestion(Manifest manifest, Guid currentQuestionId)
        {
            _manifest = manifest ?? _manifest;
            return _navigationEngine.GetLiveQuestion(manifest, currentQuestionId);
        }

        public List<Question> GetLiveQuestions(Manifest manifest, Guid currentQuestionId)
        {
            _manifest = manifest ?? _manifest;
            var list=new List<Question>();
            bool notRequired = true;

            while (notRequired)
            {
                var nextQ= GetLiveQuestion(manifest, currentQuestionId);
                if (null == nextQ)
                {
                    notRequired = false;
                }
                else
                {
                    currentQuestionId = nextQ.Id;
                    list.Add(nextQ);
                    notRequired = !nextQ.IsRequired;
                }
            }

            return list;
        }

        public Question GetNextQuestion(Guid currentQuestionId, Manifest manifest = null)
        {
            _manifest = manifest ?? _manifest;
            return _navigationEngine.GetNextQuestion(currentQuestionId,_manifest);
        }

        public Question GetPreviousQuestion(Guid currentQuestionId)
        {
            return _navigationEngine.GetPreviousQuestion(currentQuestionId,_manifest);
        }

        public Question GetQuestion(Guid questionId, Manifest currentManifest)
        {
            _manifest = currentManifest ?? _manifest;
            return _navigationEngine.GetQuestion(questionId, currentManifest);
        }

        public bool ValidateResponse(Guid encounterId, Guid questionId, object response)
        {
            var liveResponse = new Response(encounterId);

            var question = _manifest.GetQuestion(questionId);
            liveResponse.SetQuestion(question);
            liveResponse.SetObs(encounterId, questionId, question.Concept.ConceptTypeId, response);

            return _validationEngine.Validate(liveResponse);
        }

        public void SaveResponse(Guid encounterId, Guid questionId, object response, bool validated = false)
        {
            var liveResponse=new Response(encounterId);

            var question = _manifest.GetQuestion(questionId);
            liveResponse.SetQuestion(question);
            liveResponse.SetObs(encounterId, questionId, question.Concept.ConceptTypeId, response);

            if (validated)
            {
                if (_validationEngine.Validate(liveResponse))
                {
                    _obsRepository.SaveOrUpdate(liveResponse.Obs);
                    UpdateManifest(encounterId);
                }
            }
            else
            {
                _obsRepository.SaveOrUpdate(liveResponse.Obs);
                UpdateManifest(encounterId);
            }
        }

        public void ClearEncounter(Guid encounterId)
        {
            _encounterRepository.ClearObs(encounterId);
        }

        private void UpdateManifest(Guid encounterId)
        {
            _encounter = _encounterRepository.Load(encounterId, true);

            if (null == _encounter)
                throw new ArgumentException("Encounter has not been started");

            _manifest.UpdateEncounter(_encounter);
        }
    }
}