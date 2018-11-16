using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.SmartCard
{
    public class PSmartStore : Entity<Guid>
    {
        public string Shr { get; set; }
        public DateTime? Date_Created { get; set; }
        public string Status { get; set; }
        public DateTime? Status_Date { get; set; }
        public Guid Uuid { get; set; }
        public Guid ClientId { get; set; }

        public PSmartStore ()
        {
        }

        private PSmartStore(string shr, Guid clientId)
        {
            Shr = shr;
            Date_Created= Status_Date=DateTime.Now;
            Uuid = LiveGuid.NewGuid();
            Status = "PENDING";
            ClientId = clientId;
        }
        public void UpdateShr(PSmartStore pSmartStore)
        {
            Shr = pSmartStore.Shr;
            Status_Date = DateTime.Now;
            Status = "PENDING";
        }
        public static PSmartStore Create(string shr, Guid clientId)
        {
            var pstore =new PSmartStore(shr,clientId);
            return pstore;
        }
    }
}