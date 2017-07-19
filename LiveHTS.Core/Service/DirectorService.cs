using System;
using System.Linq;
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
            var form= _formRepository.GetWithQuestions(_encounter.FormId,true);
            _manifest = Manifest.Create(form, _encounter);
        }

        public void UpdateManifest()
        {
            _encounter = _encounterRepository.Get(_encounter.Id);
            _manifest.UpdateEncounter(_encounter);
        }

        public Question GetLiveQuestion()
        {
            Question question = null;

            if( !_manifest.HasQuestions())
                throw new ArgumentException("No Questions Found");

            //Get FIRST Question
            if (!_manifest.HasResponses())
                question = _manifest.GetFirstQuestion();
                

            //GetLastResponse
            var lastResonse = _manifest.GetLastResponse();

            //Check Branch Directions
            if (lastResonse.Question.HasBranches)
            {
                //Post
                var postBranches =lastResonse.Question.Branches.Where(x => x.ConditionId.ToLower() == "Post".ToLower()).ToList();
                if (postBranches.Count > 0)
                {
                    foreach (var questionBranch in postBranches)
                    {
                        var nextQuestionId = questionBranch.Evaluate(lastResonse.Obs);

                    }
                }
            }



            return question;
        }
    }
}