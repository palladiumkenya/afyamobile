namespace LiveHTS.Core.Model.SmartCard
{
    public class PROVIDERDETAILS
    {
        public string NAME { get; set; }
        public string ID { get; set; }

        public PROVIDERDETAILS()
        {
            NAME = ID = string.Empty;
        }

        public PROVIDERDETAILS(string name, string id)
        {
            NAME = name;
            ID = id;
        }

        public override string ToString()
        {
            return $"{NAME} ({ID})";
        }
    }
}