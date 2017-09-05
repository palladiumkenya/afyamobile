using System;
using LiveHTS.Core.Model.Interview;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class QuestionTransformation : Entity<Guid>
    {
        [Indexed]
        public string ConditionId { get; set; }
        [Indexed]
        public Guid? RefQuestionId { get; set; }
        public string ResponseType { get; set; }
        public string Response { get; set; }
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        [Indexed]
        public string ActionId { get; set; }
        public decimal? Rank { get; set; }
        public string Content { get; set; }
        [Indexed]
        public Guid QuestionId { get; set; }

        public QuestionTransformation()
        {
            Id = LiveGuid.NewGuid();
        }

        public SetResponse Evaluate(ObsValue current)
        {
            if (ResponseType.Equals("="))
            {
                object responseObject = Response;

                if (current.Type == typeof(Guid?))
                {
                    responseObject = new Guid(Response);
                }
                if (current.Type == typeof(decimal?))
                {
                    responseObject = Convert.ToDecimal(Response);
                }
                if (current.Type == typeof(DateTime?))
                {
                    responseObject = Convert.ToDateTime(Response);
                }

                return responseObject.Equals(current.Value) ? new SetResponse(RefQuestionId,ActionId,Content, Rank, ConditionId, ResponseComplex) : null;
            }
            return null;
        }

        public SetResponse GetComplex()
        {
            return new SetResponse(RefQuestionId, ActionId, Content, Rank, ConditionId, ResponseComplex);
        }

        public override string ToString()
        {
            return $"{ConditionId},{RefQuestionId}{ResponseType}{Response}{ActionId}";
        }
    }
}