using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Model.SmartCard
{
    public class CARDDETAILS
    {
        public string STATUS { get; set; }
        public string REASON { get; set; }
        public string LAST_UPDATED { get; set; }
        public string LAST_UPDATED_FACILITY { get; set; }

        public CARDDETAILS()
        {
            STATUS = "ACTIVE";
            REASON = LAST_UPDATED = LAST_UPDATED_FACILITY = string.Empty;
        }

        private CARDDETAILS(string status, string reason, string lastUpdated, string lastUpdatedFacility):this(lastUpdated,lastUpdatedFacility)
        {
            STATUS = status;
            REASON = reason;
        }

        private CARDDETAILS(string lastUpdated, string lastUpdatedFacility)
        {
            LAST_UPDATED = lastUpdated;
            LAST_UPDATED_FACILITY = lastUpdatedFacility;
        }
        public static CARDDETAILS Create(string status, string reason, string lastUpdated, string lastUpdatedFacility)
        {
            return new CARDDETAILS(status,reason,lastUpdated, lastUpdatedFacility);
        }
        public static CARDDETAILS Create(string lastUpdated, string lastUpdatedFacility)
        {
            return new CARDDETAILS(lastUpdated,lastUpdatedFacility);
        }

        public bool IsNew()
        {
            return !string.IsNullOrWhiteSpace(STATUS) && STATUS.IsSameAs("ACTIVE") &&
                   string.IsNullOrWhiteSpace(REASON) &&
                   string.IsNullOrWhiteSpace(LAST_UPDATED) &&
                   string.IsNullOrWhiteSpace(LAST_UPDATED_FACILITY);
        }

        public void UpdateTo(CARDDETAILS carddetails)
        {
            LAST_UPDATED = carddetails.LAST_UPDATED;
            LAST_UPDATED_FACILITY = carddetails.LAST_UPDATED_FACILITY;
        }
    }
}