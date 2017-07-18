using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class Encounter:Entity<Guid>
    {
        [Indexed]
        public Guid ClientId { get; set; }
        [Indexed]
        public Guid FormId { get; set; }
        [Indexed]
        public Guid EncounterTypeId { get; set; }
        public DateTime EncounterDate { get; set; }
        [Indexed]
        public Guid UserId { get; set; }
        [Indexed]
        public Guid DeviceId { get; set; }
        [Indexed]
        public Guid PracticeId { get; set; }
        public IEnumerable<Obs> Obses { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Stopped { get; set; }
    }
}