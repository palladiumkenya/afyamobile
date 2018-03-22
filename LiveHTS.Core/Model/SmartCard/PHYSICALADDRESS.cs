namespace LiveHTS.Core.Model.SmartCard
{
    public class PHYSICALADDRESS
    {
        public string VILLAGE { get; set; }
        public string WARD { get; set; }
        public string SUB_COUNTY { get; set; }
        public string COUNTY { get; set; }
        public string NEAREST_LANDMARK { get; set; }

        public PHYSICALADDRESS()
        {
        }

        public void UpdateTo(string landmark)
        {
            NEAREST_LANDMARK = landmark;
        }
    }
}