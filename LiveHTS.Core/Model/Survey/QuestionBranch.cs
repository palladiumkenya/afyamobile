using System;
using LiveHTS.Core.Model.Interview;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class QuestionBranch : Entity<Guid>
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
        [Indexed]
        public Guid? GotoQuestionId { get; set; }
        [Indexed]
        public Guid QuestionId { get; set; }

        public QuestionBranch()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{ConditionId},{ResponseType}{Response}>>{GotoQuestionId}";
        }
        public Guid? Evaluate(ObsValue current)
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

                return responseObject.Equals(current.Value) ? GotoQuestionId : null;
            }
            return null;
        }

        //TODO:Pre Branches Evaluate
        public Guid? Evaluate(ObsValue other,ObsValue current)
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

                return responseObject.Equals(current.Value) ? GotoQuestionId : null;
            }
            return null;
        }

    }
}