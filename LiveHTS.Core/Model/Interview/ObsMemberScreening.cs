using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsMemberScreening : Entity<Guid>
    {
        public DateTime ScreeningDate { get; set; }
        public Guid HivStatus { get; set; }
        public Guid Eligibility { get; set; }
        public DateTime BookingDate { get; set; }

        public string Remarks { get; set; }
        public Guid IndexClientId { get; set; }
        public Guid EncounterId { get; set; }

        public ObsMemberScreening()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsMemberScreening(Guid id,DateTime screeningDate, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks,Guid encounterId, Guid indexClientId)
        {
            Id = id;
            ScreeningDate = screeningDate;
            HivStatus = hivStatus;
            Eligibility = eligibility;
            BookingDate = bookingDate;
            Remarks = remarks;
            EncounterId = encounterId;
            IndexClientId = indexClientId;
        }

        public ObsMemberScreening(DateTime screeningDate, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId,Guid indexClientId)
        {
            ScreeningDate = screeningDate;
            HivStatus = hivStatus;
            Eligibility = eligibility;
            BookingDate = bookingDate;
            Remarks = remarks;
            EncounterId = encounterId;
            IndexClientId = indexClientId;
        }

        public static ObsMemberScreening Create(DateTime screeningDate, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId, Guid indexClientId)
        {
            var obs = new ObsMemberScreening(LiveGuid.NewGuid(),screeningDate,hivStatus,eligibility,bookingDate,remarks,encounterId,indexClientId);
            return obs;
        }
        public static ObsMemberScreening Create(Guid id, DateTime screeningDate, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId, Guid indexClientId)
        {
            var obs = new ObsMemberScreening(id, screeningDate, hivStatus, eligibility, bookingDate, remarks, encounterId,indexClientId);
            return obs;
        }
    }
}