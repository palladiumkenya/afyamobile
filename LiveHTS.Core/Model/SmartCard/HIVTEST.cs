namespace LiveHTS.Core.Model.SmartCard
{
    public class HIVTEST
    {
        public string DATE { get; set; }
        public string RESULT { get; set; }
        public string TYPE { get; set; }
        public string FACILITY { get; set; }
        public string STRATEGY { get; set; }
        public PROVIDERDETAILS PROVIDER_DETAILS { get; set; }
    }
}