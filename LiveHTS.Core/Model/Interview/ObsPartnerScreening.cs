using System;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    //TODO: Map screening
    public class ObsPartnerScreening : Entity<Guid>
    {
        public DateTime ScreeningDate { get; set; }
        public Guid? PnsAccepted { get; set; }
        public Guid IPVScreening { get; set; }
        public Guid PhysicalAssult { get; set; }
        public Guid Threatened { get; set; }
        public Guid SexuallyUncomfortable { get; set; }
        public Guid? IPVOutcome { get; set; }
        public string Occupation { get; set; }
        public Guid? PNSRealtionship { get; set; }
        public Guid? LivingWithClient { get; set; }
        public Guid HivStatus { get; set; }

        public Guid? PNSApproach { get; set; }
        public Guid Eligibility { get; set; }
        public DateTime BookingDate { get; set; }
        public string Remarks { get; set; }
   
    

     

        public Guid EncounterId { get; set; }

        public ObsPartnerScreening()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsPartnerScreening(DateTime screeningDate, Guid ipvScreening, Guid physicalAssult, Guid threatened,
            Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility, DateTime bookingDate, string remarks,
            Guid? pnsAccepted, Guid? iPVOutcome, string occupation, Guid? pNSRealtionship, Guid? livingWithClient, Guid? pNSApproach,
            Guid encounterId)
        {
            ScreeningDate = screeningDate;

            IPVScreening = ipvScreening;
            PhysicalAssult = physicalAssult;
            Threatened = threatened;
            SexuallyUncomfortable = sexuallyUncomfortable;
            HivStatus = hivStatus;

            Eligibility = eligibility;

            PnsAccepted = pnsAccepted;
            IPVOutcome = iPVOutcome;
            Occupation = occupation;
            PNSRealtionship = pNSRealtionship;
            LivingWithClient = livingWithClient;
            PNSApproach = pNSApproach;

            BookingDate = bookingDate;
            Remarks = remarks;
            EncounterId = encounterId;
        }

        public ObsPartnerScreening(Guid id, DateTime screeningDate, Guid ipvScreening, Guid physicalAssult,
            Guid threatened, Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility, DateTime bookingDate,
            string remarks,
            Guid? pnsAccepted, Guid? iPVOutcome, string occupation, Guid? pNSRealtionship, Guid? livingWithClient, Guid? pNSApproach,
            Guid encounterId)
            : this(screeningDate, ipvScreening, physicalAssult, threatened, sexuallyUncomfortable, hivStatus,
                eligibility, bookingDate, remarks,
                pnsAccepted,iPVOutcome,occupation,pNSRealtionship,livingWithClient,pNSApproach,
                encounterId)
        {
            Id = id;
        }

        public static ObsPartnerScreening Create(DateTime screeningDate, Guid ipvScreening, Guid physicalAssult,
            Guid threatened, Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility, DateTime bookingDate,
            string remarks,
            Guid? pnsAccepted, Guid? iPVOutcome, string occupation, Guid? pNSRealtionship, Guid? livingWithClient, Guid? pNSApproach,
            Guid encounterId)
        {
            var obs = new ObsPartnerScreening(LiveGuid.NewGuid(), screeningDate, ipvScreening, physicalAssult,
                threatened, sexuallyUncomfortable, hivStatus, eligibility, bookingDate, remarks,
                pnsAccepted, iPVOutcome, occupation, pNSRealtionship, livingWithClient, pNSApproach,
                encounterId);
            return obs;
        }

        public static ObsPartnerScreening Create(Guid id, DateTime screeningDate, Guid ipvScreening,
            Guid physicalAssult, Guid threatened, Guid sexuallyUncomfortable, Guid hivStatus, Guid eligibility,
            DateTime bookingDate, string remarks,
            Guid? pnsAccepted, Guid? iPVOutcome, string occupation, Guid? pNSRealtionship, Guid? livingWithClient, Guid? pNSApproach,
            Guid encounterId)
        {
            var obs = new ObsPartnerScreening(id, screeningDate, ipvScreening, physicalAssult, threatened,
                sexuallyUncomfortable, hivStatus, eligibility, bookingDate, remarks,
                pnsAccepted, iPVOutcome, occupation, pNSRealtionship, livingWithClient, pNSApproach,
                encounterId);
            return obs;
        }
    }
}