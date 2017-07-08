using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class QuestionBranch : Entity<Guid>
    {
        public int ConditionId { get; set; }
        public string ResponseType { get; set; }
        public string Response { get; set; }
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        public int ActionId { get; set; }
        public Guid? GotoQuestionId { get; set; }
        public Guid QuestionId { get; set; }
    }
}