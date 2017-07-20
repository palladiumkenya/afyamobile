using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service
{
    public class DirectorService : IDirectorService
    {
        private readonly IFormRepository _formRepository;
        private readonly IEncounterRepository _encounterRepository;
        private Encounter _encounter;
        private Manifest _manifest;
        private readonly Response _liveResponse;

        public Manifest Manifest
        {
            get { return _manifest; }
        }

        public Response LiveResponse
        {
            get { return _liveResponse; }
        }

        public DirectorService(IFormRepository formRepository, IEncounterRepository encounterRepository,
            Encounter encounter)
        {
            _formRepository = formRepository;
            _encounterRepository = encounterRepository;
            _encounter = encounter;
            _liveResponse=new Response(encounter.Id);
        }

        public void Initialize()
        {
            var form = _formRepository.GetWithQuestions(_encounter.FormId, true);
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

            if (!_manifest.HasQuestions())
                throw new ArgumentException("No Fields in Form");

            //Get FIRST Question
            if (!_manifest.HasResponses())
                return _manifest.GetFirstQuestion();

            //GetLastResponse
            var lastResonse = _manifest.GetLastResponse();

            //Check for Branch Directions
            var lastQuestion = lastResonse.Question;

            if (null != lastQuestion)
            {
                //TODO:Pre Branches

                #region Pre Branches

                //                if (lastQuestion.HasBranches)
                //                {
                //                    var preBranches = lastQuestion.Branches.Where(x => x.ConditionId.ToLower() == "Pre".ToLower())
                //                        .ToList();
                //
                //                    foreach (var questionBranch in preBranches)
                //                    {
                //                        var nextQuestionId = questionBranch.Evaluate(lastResonse.GetValue());
                //                        if (!nextQuestionId.IsNullOrEmpty())
                //                        {
                //                            question = _manifest.GetQuestion(nextQuestionId.Value);
                //                            break;
                //                        }
                //                    }
                //                }

                #endregion

                #region Post Branches

                if (lastQuestion.HasBranches)
                {
                    var postBranches = lastQuestion.Branches.Where(x => x.ConditionId.ToLower() == "Post".ToLower()).ToList();
                    foreach (var questionBranch in postBranches)
                    {
                        var nextQuestionId = questionBranch.Evaluate(lastResonse.GetValue());
                        if (!nextQuestionId.IsNullOrEmpty())
                        {
                            question = _manifest.GetQuestion(nextQuestionId.Value);
                            break;
                        }
                    }
                }
                #endregion

                //Get Next if not in Branch
                if (null == question)
                    question = _manifest.GetNextRankQuestionAfter(lastQuestion.Id);

                //TODO:Review NextQuestion
            }

            _liveResponse.SetQuestion(question);

            return question;
        }

        public bool ValidateResponse()
        {
            if (!_liveResponse.Question.HasValidations)
                return true;

            var validations = _liveResponse.Question.Validations;

            foreach (var validation in validations)
            {

            }

            throw new NotImplementedException();
        }
    }
}