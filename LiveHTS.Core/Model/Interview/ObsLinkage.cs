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
        public string EnrollmentId { get; set; }
        public string Remarks { get; set; }
        public Guid EncounterId { get; set; }

        public ObsLinkage()
        {
            Id = LiveGuid.NewGuid();
        }

        public ObsLinkage(string referredTo, DateTime? datePromised, string handedTo, string workerCarde, DateTime? dateEnrolled, string enrollmentId, string remarks, Guid encounterId):this()
        {
            ReferredTo = referredTo;
            DatePromised = datePromised;
            HandedTo = handedTo;
            WorkerCarde = workerCarde;
            DateEnrolled = dateEnrolled;
            EnrollmentId = enrollmentId;
            Remarks = remarks;
            EncounterId = encounterId;
        }

        public static ObsLinkage Create(string referredTo, DateTime? datePromised, string handedTo, string workerCarde, DateTime? dateEnrolled, string enrollmentId, string remarks, Guid encounterId)
        {
            var obs=new ObsLinkage( referredTo,  datePromised,  handedTo,  workerCarde,  dateEnrolled,  enrollmentId,  remarks,  encounterId);
            return obs;
        }
        public static ObsLinkage Create(Guid id, string referredTo, DateTime? datePromised, string handedTo, string workerCarde, DateTime? dateEnrolled, string enrollmentId, string remarks, Guid encounterId)
        {
            var obs = new ObsLinkage(referredTo, datePromised, handedTo, workerCarde, dateEnrolled, enrollmentId, remarks, encounterId);
            obs.Id = id;
            return obs;
        }
    }
}