using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Engine;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Engine
{
    public class NavigationEngine : INavigationEngine
    {
        public List<SetResponse> GetActions(Manifest currentManifest, Guid currentQuestionId)
        {
            List<SetResponse> actions=new  List<SetResponse>();
            Question lastQuestion = null;
            Response latestResponse = null;

            if (!currentManifest.HasQuestions())
                throw new ArgumentException("No Fields in Form");

            lastQuestion = currentManifest.GetQuestion(currentQuestionId);
            latestResponse = currentManifest.GetResponse(currentQuestionId);
            
            if (null != lastQuestion&&null!= latestResponse)
            {

                if (lastQuestion.HasConditionalTransformations("Post"))
                {
                    var transformations = lastQuestion.Transformations
                        .Where(x => x.ConditionId.ToLower() == "Post".ToLower())
                        .OrderBy(x => x.Rank)
                        .ToList();


                    foreach (var transformation in transformations)
                    {
                        var setResponse = transformation.Evaluate(latestResponse.GetValue());
                        if (null != setResponse)
                        {
                            actions.Add(setResponse);
                        }
                    }
                }
            }

            return actions;
        }

        public List<SetResponse> GetRemoteActions(Manifest currentManifest, Guid currentQuestionId)
        {
            throw new NotImplementedException();
        }


        public List<SetResponse> GetActionsComplex(Manifest currentManifest, Guid currentQuestionId)
        {
            List<SetResponse> actions = new List<SetResponse>();
            Question lastQuestion = null;
            

            if (!currentManifest.HasQuestions())
                throw new ArgumentException("No Fields in Form");

            lastQuestion = currentManifest.GetQuestion(currentQuestionId);
            

            if (null != lastQuestion)
            {

                if (lastQuestion.HasTransformations)
                {
                    var transformations = lastQuestion.Transformations
                        .OrderBy(x => x.Rank)
                        .ToList();


                    foreach (var transformation in transformations)
                    {
                        if (!string.IsNullOrWhiteSpace(transformation.ResponseComplex))
                        {
                            actions.Add(transformation.GetComplex());
                        }
                    }
                }
            }

            return actions;
        }

        public Question GetLiveQuestion(Manifest currentManifest, Guid? currentQuestionId = null)
        {
            Question lastQuestion = null;
            Question candidateQuestion = null;
            Response latestResponse;

            if (!currentManifest.HasQuestions())
                throw new ArgumentException("No Fields in Form");

            
            //Last Response
            if (!currentQuestionId.IsNullOrEmpty())
            {
                 latestResponse = currentManifest.GetResponse(currentQuestionId.Value);
            }
            else
            {
                latestResponse = currentManifest.GetLastResponse();
            }            

            // return FIRST Question

            if (null == latestResponse && !currentManifest.HasResponses())
            {
                candidateQuestion = currentManifest.GetFirstQuestion();
                return candidateQuestion;
            }

            // return NEXT Question without evaluation
            
            if (null == latestResponse && currentManifest.HasResponses()&& !currentQuestionId.IsNullOrEmpty())
            {             
                lastQuestion = currentManifest.GetQuestion(currentQuestionId.Value);
            }
            else
            {             
                lastQuestion = latestResponse.Question;
            }                       

            // Vet last Question
            if (null != lastQuestion)
            {
               
                #region Post Branches

                if (lastQuestion.HasConditionalBranches("Post") && null != latestResponse)
                {
                    var postBranches = lastQuestion.Branches.Where(x => x.ConditionId.ToLower() == "Post".ToLower())
                        .ToList();
                    foreach (var questionBranch in postBranches)
                    {
                        var gotoQuestionId = questionBranch.Evaluate(latestResponse.GetValue());
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

            return EvaluateSelf(candidateQuestion, currentManifest, currentQuestionId);
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
        public Question EvaluateSelf(Question question, Manifest currentManifest, Guid? currentQuestionId = null)
        {
            Response last;

            if (null == question)
            {
                currentManifest.EndQuestionId = currentQuestionId;
                currentManifest.ReachedEndQuestion = true;
                return null;
            }
                

            Question nextQuestion = question;
            nextQuestion.SkippedQuestionIds = new List<Guid>();

            //TODO: Pre Branches          

            //TODO: Transformations

            //TODO: RemoteTransformations

            //TODO: ReVailidations

            //Last Response

            if (!currentQuestionId.IsNullOrEmpty())
            {
                last = currentManifest.GetResponse(currentQuestionId.Value);
            }
            else
            {
                last = currentManifest.GetLastResponse();
            }

            
            if (null == last || null==nextQuestion)
                return nextQuestion;

            var lastQ = last.Question;

            if (!lastQ.HasConditionalBranches("Post"))
            {
                return nextQuestion;
            }

            

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