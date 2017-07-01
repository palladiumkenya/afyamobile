using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Concept:Entity
    {
        public string Display { get; set; }
        public string Description { get; set; }
        public decimal Rank { get; set; }
        public Guid ConceptTypeId { get; set; }
        public Guid? LookupConceptId { get; set; }
        public Guid SectionId { get; set; }        
    }
}