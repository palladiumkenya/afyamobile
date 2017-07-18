using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Custom;
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
        public Guid ProviderId { get; set; }
        [Indexed]
        public Guid DeviceId { get; set; }
        [Indexed]
        public Guid PracticeId { get; set; }
        
        public DateTime? Started { get; set; }
        public DateTime? Stopped { get; set; }
        [Ignore]
        public IEnumerable<Obs> Obses { get; set; }
        [Indexed]
        public Guid UserId { get; set; }

        public Encounter()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}