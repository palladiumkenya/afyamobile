using System;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class Concept:Entity<Guid>
    {
        public string Name { get; set; }
        [Indexed]
        public int ConceptTypeId { get; set; }
        [Indexed]
        public Guid? LookupCategoryId { get; set; }
    }
}