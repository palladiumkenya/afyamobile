namespace LiveHTS.Core.Model.SmartCard
{
    public class INTERNALPATIENTID
    {
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }
        public string ASSIGNING_FACILITY { get; set; }

        public INTERNALPATIENTID()
        {
        }

        private INTERNALPATIENTID(string id, string assigningFacility)
        {
            ID = id;
            IDENTIFIER_TYPE = "HTS_NUMBER";
            ASSIGNING_AUTHORITY = "HTS";
            ASSIGNING_FACILITY = assigningFacility;
        }

        public static INTERNALPATIENTID Create(string id, string assigningFacility)
        {
            return new INTERNALPATIENTID(id,assigningFacility);
        }
    }
}