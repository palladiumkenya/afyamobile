using System;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class QuestionTransformation : Entity<Guid>
    {
        [Indexed]
        public int ConditionId { get; set; }
        [Indexed]
        public Guid? RefQuestionId { get; set; }
        public string ResponseType { get; set; }
        public string Response { get; set; }
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        [Indexed]
        public int ActionId { get; set; }
        public int Content { get; set; }
        [Indexed]
        public Guid QuestionId { get; set; }
    }
}