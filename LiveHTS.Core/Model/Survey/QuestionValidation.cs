using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class QuestionValidation : Entity<Guid>
    {
        public int ValidatorId { get; set; }
        public int ValidatorTypeId { get; set; }
        public int Revision { get; set; }
        public string MinLimit { get; set; }
        public string MaxLimit { get; set; }
        public Guid QuestionId { get; set; }
    }
}