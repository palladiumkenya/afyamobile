using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Interview
{
    public class Encounter:Entity<Guid>
    {
        public Guid PersonId { get; set; }
        public Guid FormId { get; set; }
        public Guid EncounterTypeId { get; set; }
        public DateTime EncounterDate { get; set; }
    }
}