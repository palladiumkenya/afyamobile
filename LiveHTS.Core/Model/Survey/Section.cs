using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Section:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Concept> Concepts { get; set; }=new List<Concept>();
        public Guid FormId { get; set; }

        public void AddConcept(Concept concept)
        {
            concept.SectionId = Id;
            Concepts.Add(concept);
        }
        public void AddConcepts(List<Concept> concepts)
        {
            foreach (var concept in concepts)
            {
                AddConcept(concept);
            }
        }
    }
}