using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class LookupConcept:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<LookupConceptItem> ConceptItems { get; set; }
    }
}