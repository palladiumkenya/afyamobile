using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsPartnerScreening : Entity<Guid>
    {
        public DateTime ScreeningDate { get; set; }
        public Guid HivStatus { get; set; }
        public Guid IPVScreening { get; set; }
        public DateTime BookingDate { get; set; }
        public string Remarks { get; set; }
        public Guid EncounterId { get; set; }

        public ObsPartnerScreening()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsPartnerScreening(Guid id,DateTime screeningDate, Guid hivStatus, Guid ipvScreening, DateTime bookingDate, string remarks, Guid encounterId)
        {
            Id = id;
            ScreeningDate = screeningDate;
            HivStatus = hivStatus;
            IPVScreening = ipvScreening;
            BookingDate = bookingDate;
            Remarks = remarks;
            EncounterId = encounterId;
        }

        public ObsPartnerScreening(DateTime screeningDate, Guid hivStatus, Guid ipvScreening, DateTime bookingDate, string remarks, Guid encounterId)
        {
            ScreeningDate = screeningDate;
            HivStatus = hivStatus;
            IPVScreening = ipvScreening;
            BookingDate = bookingDate;
            Remarks = remarks;
            EncounterId = encounterId;
        }

        public static ObsPartnerScreening Create(DateTime screeningDate, Guid hivStatus, Guid ipvScreening, DateTime bookingDate, string remarks, Guid encounterId)
        {
            var obs = new ObsPartnerScreening(screeningDate,hivStatus,ipvScreening,bookingDate,remarks,encounterId);
            return obs;
        }
        public static ObsPartnerScreening Create(Guid id, DateTime screeningDate, Guid hivStatus, Guid ipvScreening, DateTime bookingDate, string remarks, Guid encounterId)
        {
            var obs = new ObsPartnerScreening(id, screeningDate, hivStatus, ipvScreening, bookingDate, remarks, encounterId);
            return obs;
        }
    }
}