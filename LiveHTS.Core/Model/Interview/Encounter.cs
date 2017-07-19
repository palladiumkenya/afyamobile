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
        public IEnumerable<Obs> Obses { get; set; } = new List<Obs>();
        [Indexed]
        public Guid UserId { get; set; }

        public Encounter()
        {
            Id = LiveGuid.NewGuid();
        }


        private Encounter(Guid clientId, Guid formId, Guid encounterTypeId, Guid practiceId, Guid deviceId,Guid providerId, Guid userId) :this()
        {
            ClientId = clientId;
            FormId = formId;
            EncounterTypeId = encounterTypeId;
            Started = EncounterDate = DateTime.Now;
            DeviceId = deviceId;
            ProviderId = providerId;
            PracticeId = practiceId;
            UserId = userId;
        }

        public static Encounter CreateNew(Manifest manifest, Guid practiceId, Guid deviceId, Guid providerId, Guid userId)
        {
            return CreateNew(manifest.ClientId, manifest.FormId, manifest.EncounterTypeId, practiceId, deviceId, providerId, userId);
        }

        public static Encounter CreateNew(Guid clientId, Guid formId, Guid encounterTypeId, Guid practiceId, Guid deviceId, Guid providerId, Guid userId)
        {
            var encounter=new Encounter(clientId,formId,encounterTypeId,practiceId,deviceId,providerId,userId);
            return encounter;
        }
        public override string ToString()
        {
            return $"{Id} {EncounterDate:F}";
        }
    }
}