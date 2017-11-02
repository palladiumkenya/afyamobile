using System;
using System.Collections.Generic;
using System.Linq;
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
        [Ignore]
        public IEnumerable<ObsTestResult> ObsTestResults { get; set; } = new List<ObsTestResult>();
        [Ignore]
        public IEnumerable<ObsFinalTestResult> ObsFinalTestResults { get; set; } = new List<ObsFinalTestResult>();
        [Ignore]
        public IEnumerable<ObsTraceResult> ObsTraceResults { get; set; } = new List<ObsTraceResult>();
        [Ignore]
        public IEnumerable<ObsLinkage> ObsLinkages { get; set; } = new List<ObsLinkage>();
        [Ignore]
        public IEnumerable<ObsMemberScreening> ObsMemberScreenings { get; set; } = new List<ObsMemberScreening>();
        [Ignore]
        public IEnumerable<ObsFamilyTraceResult> ObsFamilyTraceResults { get; set; } = new List<ObsFamilyTraceResult>();
        [Indexed]
        public Guid UserId { get; set; }
        public bool IsComplete { get; set; }

        [Ignore]
        public string Status
        {
            get
            {
                if (IsComplete)
                    return "Completed";

                return "Started";
            }
        }

        [Ignore]
        public bool HasObs
        {
            get { return Obses.Any(); }
        }
      


        public Encounter()
        {
            //Status = "Created";
            Id = LiveGuid.NewGuid();
            EncounterDate = DateTime.Now;
        }
        private Encounter(Guid formId, Guid encounterTypeId, Guid clientId,  Guid providerId, Guid userId,Guid practiceId,Guid deviceId):this()
        {
            FormId = formId;
            EncounterTypeId = encounterTypeId;
            ClientId = clientId;
            ProviderId = providerId;
            UserId = userId;
            PracticeId = practiceId;
            DeviceId = deviceId;
        }
        public static Encounter CreateNew(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId, Guid practiceId, Guid deviceId)
        {
            var encounter = new Encounter(formId,encounterTypeId, clientId, providerId, userId,practiceId,deviceId);
            return encounter;
        }

        public void AddOrUpdate(Obs obs,bool saveNulls=true)
        {
            var obses = Obses.ToList();

            if (obses.Any(x => x.QuestionId == obs.QuestionId))
            {
                var obsForUpdate = obses.First(x => x.QuestionId == obs.QuestionId);
                obses.Remove(obsForUpdate);
                if (saveNulls)
                {
                    obsForUpdate.UpdateFrom(obs);
                    obses.Add(obsForUpdate);
                }
                else
                {
                    if (!obs.IsNull)
                    {
                        obsForUpdate.UpdateFrom(obs);
                        obses.Add(obsForUpdate);
                    }
                }
            }
            else
            {
                obs.EncounterId = Id;
                if (saveNulls)
                {
                    obses.Add(obs);
                }
                else
                {
                    if(!obs.IsNull)
                        obses.Add(obs);
                }
            }

            Obses = obses;
        }

        public override string ToString()
        {
            return $"{Id} {EncounterDate:F} [{Status}]";
        }
    }
}