using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class QuestionTransformation : Entity<Guid>
    {
        public int ConditionId { get; set; }
        public Guid? RefQuestionId { get; set; }
        public string ResponseType { get; set; }
        public string Response { get; set; }
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        public int ActionId { get; set; }
        public int Content { get; set; }
        public Guid QuestionId { get; set; }
    }
}