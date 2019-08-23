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

        public Guid? Consent { get; set; }
        [Ignore]
        public string ConsentDisplay { get; set; }
        public DateTime? Reminder { get; set; }
        public DateTime? BookingDate { get; set; }
        public Guid IndexClientId { get; set; }
        public Guid? ReasonNotContacted { get; set; }
        [Ignore]
        public string ReasonNotContactedDisplay { get; set; }
        public string ReasonNotContactedOther { get; set; }
        public Guid EncounterId { get; set; }

        public ObsFamilyTraceResult()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsFamilyTraceResult(DateTime date, Guid mode, Guid outcome, Guid? consent, DateTime? reminder , DateTime? bookingDate ,Guid encounterId,Guid indexClientId,Guid? reasonNotContacted,string reasonNotContactedOther) : this()
        {
            Date = date;
            Mode = mode;
            Outcome = outcome;
            EncounterId = encounterId;
            Consent = consent;
            Reminder = reminder;
            BookingDate = bookingDate;
            IndexClientId = indexClientId;
            ReasonNotContacted = reasonNotContacted;
            ReasonNotContactedOther = reasonNotContactedOther;
        }

        public static ObsFamilyTraceResult Create(Guid id, DateTime date, Guid mode, Guid outcome, Guid? consent, DateTime? reminder, DateTime? bookingDate, Guid encounterId, Guid indexClientId,Guid? reasonNotContacted,string reasonNotContactedOther)
        {
            var obs = new ObsFamilyTraceResult(date, mode, outcome,consent,reminder,bookingDate, encounterId,indexClientId,reasonNotContacted,reasonNotContactedOther);
            obs.Id = id;
            return obs;
        }
        public static ObsFamilyTraceResult Create(DateTime date, Guid mode, Guid outcome, Guid? consent, DateTime? reminder, DateTime? bookingDate, Guid encounterId, Guid indexClientId,Guid? reasonNotContacted,string reasonNotContactedOther)
        {
            return new ObsFamilyTraceResult(date, mode, outcome, consent,reminder,bookingDate,encounterId,indexClientId,reasonNotContacted,reasonNotContactedOther);
        }
        public static ObsFamilyTraceResult CreateNew(DateTime date, Guid mode, Guid outcome, Guid? consent, DateTime? reminder, DateTime? bookingDate, Guid encounterId, Guid indexClientId,Guid? reasonNotContacted,string reasonNotContactedOther)
        {
            return new ObsFamilyTraceResult(date, mode, outcome,consent,reminder,bookingDate, encounterId,indexClientId,reasonNotContacted,reasonNotContactedOther);
        }
        public static ObsFamilyTraceResult CreateNew(Guid encounterId, Guid indexClientId)
        {
            var obs = new ObsFamilyTraceResult();
            obs.Date = DateTime.Today;
            obs.EncounterId = encounterId;
            obs.IndexClientId = indexClientId;
            return obs;
        }


    }
}
