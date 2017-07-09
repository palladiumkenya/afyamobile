using System;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class QuestionValidation : Entity<Guid>
    {
        [Indexed]
        public int ValidatorId { get; set; }
        [Indexed]
        public int ValidatorTypeId { get; set; }
        public int Revision { get; set; }
        public string MinLimit { get; set; }
        public string MaxLimit { get; set; }
        [Indexed]
        public Guid QuestionId { get; set; }
    }
}