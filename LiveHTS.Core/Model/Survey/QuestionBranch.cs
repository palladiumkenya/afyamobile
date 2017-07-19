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

        public Guid? Evaluate(Obs lastResonseObs)
        {
            if (ResponseType == "=")
            {
                
            }

            throw new NotImplementedException();
        }
    }
}