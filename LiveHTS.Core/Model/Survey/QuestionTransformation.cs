using System;
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
        public string Content { get; set; }
        [Indexed]
        public Guid QuestionId { get; set; }

        public QuestionTransformation()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{ConditionId},{RefQuestionId}{ResponseType}{Response}{ActionId}";
        }
    }
}