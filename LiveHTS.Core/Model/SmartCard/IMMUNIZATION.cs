using System.Collections.Generic;

namespace LiveHTS.Core.Model.SmartCard
{
    public class IMMUNIZATION
    {
        public string NAME { get; set; }
        public string DATE_ADMINISTERED { get; set; }

        public IMMUNIZATION()
        {
        }

        public static List<IMMUNIZATION> Create()
        {
            return new List<IMMUNIZATION> { new IMMUNIZATION() };
        }
    }
}