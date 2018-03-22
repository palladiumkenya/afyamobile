using System.Collections.Generic;

namespace LiveHTS.Core.Model.SmartCard
{
    public class NEXTOFKIN
    {
        public NOKNAME NOK_NAME { get; set; }
        public string RELATIONSHIP { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string SEX { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string CONTACT_ROLE { get; set; }

        public NEXTOFKIN()
        {
            NOK_NAME = new NOKNAME();
        }

        public static List<NEXTOFKIN> Create()
        {
            return new List<NEXTOFKIN> {new NEXTOFKIN()};
        }
    }
}