using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class ConceptLookup : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<ConceptLookupItem> Items { get; set; }=new List<ConceptLookupItem>();
    }
}