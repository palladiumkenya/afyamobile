namespace LiveHTS.Core.Model.SmartCard
{
    public class MOTHERNAME
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }

        public MOTHERNAME()
        {
            FIRST_NAME = MIDDLE_NAME = LAST_NAME = string.Empty;
        }

        public static MOTHERNAME Create()
        {
            return new MOTHERNAME();
        }
    }
}