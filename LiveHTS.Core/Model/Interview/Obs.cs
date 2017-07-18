using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Interview
{
    public class Obs:Entity<Guid>
    {
        public Guid QuestionId { get; set; }
        public DateTime ObsDate { get; set; }        
        public string ValueText { get; set; }
        public decimal? ValueNumeric { get; set; }
        public Guid? ValueCoded { get; set; }
        public string ValueMultiCoded { get; set; }
        public DateTime? ValueDateTime { get; set; }
        public Guid EncounterId { get; set; }       
    }
}