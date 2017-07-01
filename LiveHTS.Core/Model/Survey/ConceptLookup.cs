using System.Collections.Generic;
using System.Linq;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class ConceptLookup : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<ConceptLookupItem> Items { get; set; }=new List<ConceptLookupItem>();


        public void AddConceptLookupItem(ConceptLookupItem conceptLookupItem)
        {
            conceptLookupItem.ConceptLookupId = Id;
            Items.Add(conceptLookupItem);
        }
        public void AddConceptLookupItems(List<ConceptLookupItem> conceptLookupItems)
        {
            foreach (var conceptLookupItem in conceptLookupItems)
            {
                AddConceptLookupItem(conceptLookupItem);
            }
        }
    }
}