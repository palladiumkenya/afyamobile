using System.Collections.Generic;

namespace LiveHTS.Core.Model.SmartCard
{
    public class MOTHERDETAILS
    {
        public MOTHERNAME MOTHER_NAME { get; set; }
        public List<MOTHERIDENTIFIER> MOTHER_IDENTIFIER { get; set; }=new List<MOTHERIDENTIFIER>();

        public MOTHERDETAILS()
        {
            
        }

        public static MOTHERDETAILS Create()
        {
            var motherdetails=new MOTHERDETAILS();
            motherdetails.MOTHER_NAME=MOTHERNAME.Create();
            //motherdetails.MOTHER_IDENTIFIER = MOTHERIDENTIFIER.Create();
            return motherdetails;
        }
    }
}