using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Engine;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Engine
{
    public class SimpleDirector : IDirector
    {
        public Question GetLiveQuestion(Manifest currentManifest)
        {
            Question lastQuestion = null;
            Question candidateQuestion = null;

            if (!currentManifest.HasQuestions())
                throw new ArgumentException("No Fields in Form");

            //Last Response
            var lastResonse = currentManifest.GetLastResponse();

            // return FIRST Question
            if (null == lastResonse)
            {
                candidateQuestion = currentManifest.GetFirstQuestion();
                return candidateQuestion;
            }

            // return Next Question

            lastQuestion = lastResonse.Question;            

            // Vet last Question
            if (null != lastQuestion)
            {
                #region Post Branches

                if (lastQuestion.HasBranches)
                {
                    var postBranches = lastQuestion.Branches.Where(x => x.ConditionId.ToLower() == "Post".ToLower())
                        .ToList();
                    foreach (var questionBranch in postBranches)
                    {
                        var gotoQuestionId = questionBranch.Evaluate(lastResonse.GetValue());
                        if (!gotoQuestionId.IsNullOrEmpty())
                        {
                            candidateQuestion = currentManifest.GetQuestion(gotoQuestionId.Value);
                            break;
                        }
                    }
                }
                #endregion

                //Get Next if not in Branch
                if (null == candidateQuestion)
                    candidateQuestion = currentManifest.GetNextRankQuestionAfter(lastQuestion.Id);
            }

            return EvaluateSelf(candidateQuestion, currentManifest);
        }

        public Question GetNextQuestion(Guid currentQuestionId, Manifest currentManifest)
        {
            var q= currentManifest.GetNextRankQuestionAfter(currentQuestionId);
            return EvaluateSelf(q,currentManifest);
        }

        public Question GetPreviousQuestion(Guid currentQuestionId, Manifest currentManifest)
        {
            var q= currentManifest.GetPreviousRankQuestionBefore(currentQuestionId);
            return EvaluateSelf(q, currentManifest);
        }

        //TODO: Evaluate self
        private Question EvaluateSelf(Question question,Manifest currentManifest)
        {
            //TODO: Pre Branches

            //TODO: Transformtions

            //TODO: RemoteTransformations

            //TODO: ReVailidations

            Question nextQuestion = question;
            return nextQuestion;
        }
    }
}