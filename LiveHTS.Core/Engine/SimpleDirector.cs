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
                var q = currentManifest.GetFirstQuestion();
                candidateQuestion =  EvaluateSelf(q,lastResonse,currentManifest);
                return candidateQuestion;
            }

            // return Next Question

            lastQuestion = lastResonse.Question;            

            // Vet last Question
            if (null != lastQuestion)
            {
                //TODO:Pre Branches

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

            return candidateQuestion;
        }

        //TODO: Evaluate self
        private Question EvaluateSelf(Question question,Response response,Manifest currentManifest)
        {
            Question nextQuestion = question;
            return nextQuestion;
        }
    }
}