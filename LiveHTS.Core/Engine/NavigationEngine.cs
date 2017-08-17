using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Engine;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Engine
{
    public class NavigationEngine : INavigationEngine
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

                if (lastQuestion.HasConditionalBranches("Post"))
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

        public Question GetQuestion(Guid questionId, Manifest currentManifest)
        {
            var q = currentManifest.GetQuestion(questionId);
            return EvaluateSelf(q, currentManifest);
        }

        //TODO: Evaluate self
        public Question EvaluateSelf(Question question, Manifest currentManifest)
        {
            Question nextQuestion = question;

            //TODO: Pre Branches          

            //TODO: Transformations

            //TODO: RemoteTransformations

            //TODO: ReVailidations

            var last = currentManifest.GetLastResponse();
            if (null == last || null==nextQuestion)
                return nextQuestion;

            var lastQ = last.Question;
            var allQs = currentManifest.QuestionStore.OrderBy(x => x.Rank).ToList();

            var skippedQs = allQs
                .Where(x => x.Rank >= lastQ.Rank &&
                            x.Rank <= nextQuestion.Rank &&
                            x.Id!=lastQ.Id&&
                            x.Id!=nextQuestion.Id)
                .ToList();

            if(skippedQs.Count>0)
                nextQuestion.SkippedQuestionIds = skippedQs.Select(x => x.Id).ToList();

            return nextQuestion;
        }
    }
}