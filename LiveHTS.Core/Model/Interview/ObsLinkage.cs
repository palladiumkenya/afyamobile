using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsLinkage : Entity<Guid>
    {
        public string ReferredTo { get; set; }
        public DateTime? DatePromised { get; set; }
        public string FacilityHandedTo { get; set; }
        public string HandedTo { get; set; }
        public string WorkerCarde { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public DateTime? ARTStartDate { get; set; }
        public string EnrollmentId { get; set; }
        public string Remarks { get; set; }
        public Guid EncounterId { get; set; }

        public ObsLinkage()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsLinkage(string referredTo, DateTime? datePromised, string facilityHandedTo, string handedTo, string workerCarde, DateTime? dateEnrolled, string enrollmentId, string remarks, Guid encounterId, DateTime? artStartDate):this()
        {
            FacilityHandedTo = facilityHandedTo;
            ReferredTo = referredTo;
            DatePromised = datePromised;
            HandedTo = handedTo;
            WorkerCarde = workerCarde;
            DateEnrolled = dateEnrolled;
            EnrollmentId = enrollmentId;
            Remarks = remarks;
            EncounterId = encounterId;
            ARTStartDate = artStartDate;
        }

        public static ObsLinkage Create(string referredTo, DateTime? datePromised, string facilityHandedTo, string handedTo, string workerCarde, DateTime? dateEnrolled, string enrollmentId, string remarks, Guid encounterId, DateTime? artStartDate)
        {
            var obs = new ObsLinkage(referredTo, datePromised, handedTo, facilityHandedTo, workerCarde, dateEnrolled,
                enrollmentId, remarks, encounterId, artStartDate);
            return obs;
        }
        public static ObsLinkage Create(Guid id, string referredTo, DateTime? datePromised, string facilityHandedTo, string handedTo, string workerCarde, DateTime? dateEnrolled, string enrollmentId, string remarks, Guid encounterId, DateTime? artStartDate)
        {
            var obs = new ObsLinkage(referredTo, datePromised, handedTo, facilityHandedTo, workerCarde, dateEnrolled, enrollmentId, remarks, encounterId, artStartDate);
            obs.Id = id;
            return obs;
        }
        public static ObsLinkage CreateNew(string referredTo, DateTime? datePromised, Guid encounterId)
        {
            var obs = new ObsLinkage();
            obs.ReferredTo = referredTo;
            obs.DatePromised = datePromised;
            obs.EncounterId = encounterId;
            return obs;
        }

        public static ObsLinkage CreateNew(string facilityHandedTo, string handedTo, string workerCarde, DateTime? dateEnrolled, string enrollmentId, string remarks, Guid encounterId, DateTime? artStartDate)
        {
            var obs = new ObsLinkage();
            obs.FacilityHandedTo = facilityHandedTo;
            obs.HandedTo = handedTo;
            obs.WorkerCarde = workerCarde;
            obs.DateEnrolled = dateEnrolled;
            obs.ARTStartDate = artStartDate;
            obs.EnrollmentId = enrollmentId;
            obs.Remarks = remarks;
            return obs;
        }
    }
}