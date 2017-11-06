using System;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsPartnerScreening : Entity<Guid>
    {
        public DateTime ScreeningDate { get; set; }
        public Guid IPVScreening { get; set; }
        public Guid PhysicalAssult { get; set; }
        public Guid Threatened { get; set; }
        public Guid SexuallyUncomfortable { get; set; }
        public Guid HivStatus { get; set; }
        public Guid Eligibility { get; set; }
        public DateTime BookingDate { get; set; }
        public string Remarks { get; set; }
        public Guid EncounterId { get; set; }

        public ObsPartnerScreening()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsPartnerScreening(DateTime screeningDate,Guid ipvScreening, Guid physicalAssult, Guid threatened, Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId)
        {
            ScreeningDate = screeningDate;
            
            IPVScreening = ipvScreening;
            PhysicalAssult = physicalAssult;
            Threatened = threatened;
            SexuallyUncomfortable = sexuallyUncomfortable;
            HivStatus = hivStatus;
            
            Eligibility = eligibility;
            BookingDate = bookingDate;
            Remarks = remarks;
            EncounterId = encounterId;
        }

        public ObsPartnerScreening(Guid id, DateTime screeningDate, Guid ipvScreening, Guid physicalAssult, Guid threatened, Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId) 
            : this(screeningDate,  ipvScreening, physicalAssult, threatened, sexuallyUncomfortable, hivStatus,  eligibility, bookingDate, remarks, encounterId)
        {
            Id = id;
        }

        public static ObsPartnerScreening Create(DateTime screeningDate, Guid ipvScreening, Guid physicalAssult, Guid threatened, Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId)
        {
            var obs = new ObsPartnerScreening(screeningDate, ipvScreening, physicalAssult, threatened, sexuallyUncomfortable, hivStatus, eligibility, bookingDate, remarks, encounterId);
            return obs;
        }
        public static ObsPartnerScreening Create(Guid id, DateTime screeningDate, Guid ipvScreening, Guid physicalAssult, Guid threatened, Guid sexuallyUncomfortable, Guid hivStatus,  Guid eligibility, DateTime bookingDate, string remarks, Guid encounterId)
        {
            var obs = new ObsPartnerScreening(id, screeningDate, ipvScreening, physicalAssult, threatened, sexuallyUncomfortable, hivStatus, eligibility, bookingDate, remarks, encounterId);
            return obs;
        }
    }
}