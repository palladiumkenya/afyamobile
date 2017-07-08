using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Concept:Entity<Guid>
    {
        public string Name { get; set; }
        public int ConceptTypeId { get; set; }
        public Guid? LookupCategoryId { get; set; }
    }
}