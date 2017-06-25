using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class LookupConceptItem:Entity
    {
        public Guid LookupConceptId { get; set; }
        public Guid LookupItemId { get; set; }
        public string Display { get; set; }
        public decimal Rank { get; set; }
    }
}