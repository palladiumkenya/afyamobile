using System;
using System.Collections.Generic;

namespace LiveHTS.Core.Model.Survey
{
    public class Concept
    {
        public string Display { get; set; }
        public string Description { get; set; }
        public Guid ConceptTypeId { get; set; }
        public Guid? LookupConceptId { get; set; }
        public Guid ParentConceptId { get; set; }
        public IEnumerable<Concept> ChildConcepts { get; set; }=new List<Concept>();
        public IEnumerable<ConceptConfig> Configs { get; set; } = new List<ConceptConfig>();
        public Guid SectionId { get; set; }        
    }
}