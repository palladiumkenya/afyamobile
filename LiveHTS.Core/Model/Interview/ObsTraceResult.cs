using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsTraceResult : Entity<Guid>
    {
        public DateTime Date { get; set; }
        [Indexed]
        public Guid Mode { get; set; }
        [Ignore]
        public string ModeDisplay { get; set; }
        [Indexed]
        public Guid Outcome { get; set; }
        [Ignore]
        public string OutcomeDisplay { get; set; }
        public Guid? ReasonNotContacted { get; set; }
        [Ignore]
        public string ReasonNotContactedDisplay { get; set; }
        public string ReasonNotContactedOther { get; set; }
        public Guid EncounterId { get; set; }

        public ObsTraceResult()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsTraceResult(DateTime date, Guid mode, Guid outcome, Guid encounterId,Guid? reasonNotContacted,string reasonNotContactedOther):this()
        {
            Date = date;
            Mode = mode;
            Outcome = outcome;
            EncounterId = encounterId;
            ReasonNotContacted = reasonNotContacted;
            ReasonNotContactedOther = reasonNotContactedOther;
        }

        public static ObsTraceResult Create(Guid id, DateTime date, Guid mode, Guid outcome, Guid encounterId,Guid? reasonNotContacted,string reasonNotContactedOther)
        {
            var obs=new ObsTraceResult(date,mode,outcome,encounterId,reasonNotContacted,reasonNotContactedOther);
            obs.Id = id;
            return obs;
        }
        public static ObsTraceResult Create(DateTime date, Guid mode, Guid outcome, Guid encounterId,Guid? reasonNotContacted,string reasonNotContactedOther)
        {
            return new ObsTraceResult(date, mode, outcome, encounterId,reasonNotContacted,reasonNotContactedOther);
        }
        public static ObsTraceResult CreateNew(DateTime date, Guid mode, Guid outcome, Guid encounterId,Guid? reasonNotContacted,string reasonNotContactedOther)
        {
            return new ObsTraceResult(date, mode, outcome, encounterId,reasonNotContacted,reasonNotContactedOther);
        }
        public static ObsTraceResult CreateNew(Guid encounterId)
        {
            var obs= new ObsTraceResult();
            obs.Date=DateTime.Today;
            obs.EncounterId = encounterId;
            return obs;
        }


    }
}
