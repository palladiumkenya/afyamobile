using System;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class Concept:Entity<Guid>
    {
        public string Name { get; set; }
        [Indexed]
        public string ConceptTypeId { get; set; }
        [Indexed]
        public Guid? CategoryId { get; set; }
        [Ignore]
        public Category Category { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}