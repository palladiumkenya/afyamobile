using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsFamilyTraceResult : Entity<Guid>
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
        public Guid EncounterId { get; set; }

        public ObsFamilyTraceResult()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsFamilyTraceResult(DateTime date, Guid mode, Guid outcome, Guid encounterId) : this()
        {
            Date = date;
            Mode = mode;
            Outcome = outcome;
            EncounterId = encounterId;
        }

        public static ObsFamilyTraceResult Create(Guid id, DateTime date, Guid mode, Guid outcome, Guid encounterId)
        {
            var obs = new ObsFamilyTraceResult(date, mode, outcome, encounterId);
            obs.Id = id;
            return obs;
        }
        public static ObsFamilyTraceResult Create(DateTime date, Guid mode, Guid outcome, Guid encounterId)
        {
            return new ObsFamilyTraceResult(date, mode, outcome, encounterId);
        }
        public static ObsFamilyTraceResult CreateNew(DateTime date, Guid mode, Guid outcome, Guid encounterId)
        {
            return new ObsFamilyTraceResult(date, mode, outcome, encounterId);
        }
        public static ObsFamilyTraceResult CreateNew(Guid encounterId)
        {
            var obs = new ObsFamilyTraceResult();
            obs.Date = DateTime.Today;
            obs.EncounterId = encounterId;
            return obs;
        }


    }
}