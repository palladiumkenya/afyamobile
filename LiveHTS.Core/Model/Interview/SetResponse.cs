using System;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Model.Interview
{
    public class SetResponse
    {
        public Guid? QuestionId { get; set; }
        public string Action { get; set; }
        public string Response { get; set; }
        public decimal? Rank { get; set; }

        public SetResponse()
        {
        }

        public SetResponse(Guid? questionId, string action, string response, decimal? rank)
        {
            QuestionId = questionId;
            Action = action;
            Response = response;
            Rank = rank;
        }
    }
}