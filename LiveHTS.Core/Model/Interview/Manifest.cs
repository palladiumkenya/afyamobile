using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Model.Interview
{
    public class Manifest
    {
        public Encounter Encounter { get; set; }
        public List<Question> QuestionStore { get; set; } = new List<Question>();
        public List<Response> ResponseStore { get; set; } = new List<Response>();
        public Guid? EndQuestionId { get; set; }
        public bool ReachedEndQuestion { get; set; }
        public bool HasResponseErrors { get; set; }

        public Manifest()
        {
        }
        private Manifest(List<Question> questions, List<Response> responses, Encounter encounter)
        {
            QuestionStore = questions;
            ResponseStore = responses;
            Encounter = encounter;
            UpdateResponseQuestions();
        }

        public static Manifest Create(Form form, Encounter encounter)
        {
            var formWithQuestions = form ?? new Form();
            var responses = ReadResponses(encounter);

            return new Manifest(formWithQuestions.Questions, responses, encounter);
        }

        public bool HasQuestions()
        {
            return null != QuestionStore && QuestionStore.Any();
        }
        public bool HasResponses()
        {
            return null != ResponseStore && ResponseStore.Any();
        }

        public void UpdateEncounter(Encounter encounter)
        {
            Encounter = encounter;
            ResponseStore = ReadResponses(Encounter);
            UpdateResponseQuestions();
        }

        public Question GetQuestion(Guid value)
        {
            return QuestionStore.FirstOrDefault(x => x.Id == value);
        }
        public Question GetFirstQuestion()
        {
            return QuestionStore.OrderBy(x => x.Rank).FirstOrDefault();
        }
        public Question GetNextRankQuestionAfter(Guid currentQuestionId)
        {
            var currenQuestion = GetQuestion(currentQuestionId);
            var currentRank = currenQuestion.Rank;
            var currentId = currenQuestion.Id;

            return QuestionStore
                .OrderBy(x => x.Rank)
                .FirstOrDefault(x => x.Rank >= currentRank &&
                                     x.Id != currentId);
        }
        public Question GetPreviousRankQuestionBefore(Guid currentQuestionId)
        {
            var currenQuestion = GetQuestion(currentQuestionId);
            var currentRank = currenQuestion.Rank;
            var currentId = currenQuestion.Id;

            return QuestionStore
                .OrderByDescending(x=>x.Rank)
                .FirstOrDefault(x => x.Rank <= currentRank &&
                                     x.Id != currentId);
        }
        public Response GetResponse(Guid questionId)
        {
            return HasResponses() ? ResponseStore.FirstOrDefault(x => x.QuestionId==questionId) : null;
        }
        public Response GetLastResponse()
        {
            return HasResponses() ? ResponseStore.OrderBy(x => x.Question.Rank).LastOrDefault() : null;
        }

        public bool IsComplete()
        {
            if (
                HasResponses() &&
                ReachedEndQuestion &&
                !EndQuestionId.IsNullOrEmpty()
            )
            {
                var lastQ = GetQuestion(EndQuestionId.Value);
                if (lastQ.IsRequired)
                {
                    var lastResponse = GetResponse(EndQuestionId.Value);

                    var response = lastResponse.GetValue().Value;
                    return null!=response&&!string.IsNullOrWhiteSpace(response.ToString());
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateResponseQuestions()
        {
            foreach (var response in ResponseStore)
            {
                response.Question = GetQuestion(response.QuestionId);
            }
        }
        private static List<Response> ReadResponses(Encounter encounter)
        {
            var answeredQuestions = new List<Response>();

            if (null != encounter)
            {
                answeredQuestions = encounter.Obses.Select(x => new Response
                    {
                        EncounterId = x.EncounterId,
                        ObsId = x.Id,
                        Obs = x,
                        QuestionId = x.QuestionId
                    })
                    .ToList();
            }

            return answeredQuestions;
        }

        public override string ToString()
        {
            var stats = $"{ResponseStore.Count}/{QuestionStore.Count}";
            var summary = Encounter.IsComplete ? " Completed" : " Open";
            return $" Status:{stats} ,{summary}";
        }
    }
} 