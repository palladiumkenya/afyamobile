using System.Collections.Generic;

namespace LiveHTS.Core.Model.SmartCard
{
    public class MOTHERIDENTIFIER
    {
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }
        public string ASSIGNING_FACILITY { get; set; }

        public MOTHERIDENTIFIER()
        {
        }

        public static List<MOTHERIDENTIFIER> Create()
        {
            return new List<MOTHERIDENTIFIER> {new MOTHERIDENTIFIER()};
        }
    }
}