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


        public void UpdateTo(CARDDETAILS carddetails)
        {
            LAST_UPDATED = carddetails.LAST_UPDATED;
            LAST_UPDATED_FACILITY = carddetails.LAST_UPDATED_FACILITY;
        }
    }
}