using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class ConceptLookupItem : Entity
    {
        public string Display { get; set; }
        public string Description { get; set; }
        public decimal Rank { get; set; }
        public Guid ConceptLookupId { get; set; }
    }
}