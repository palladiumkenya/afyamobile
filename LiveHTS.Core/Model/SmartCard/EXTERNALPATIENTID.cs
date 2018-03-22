namespace LiveHTS.Core.Model.SmartCard
{
    public class EXTERNALPATIENTID
    {
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }
        public string ASSIGNING_FACILITY { get; set; }

        public EXTERNALPATIENTID()
        {
        }

        public static EXTERNALPATIENTID Create()
        {
            return new EXTERNALPATIENTID();
        }
    }
}