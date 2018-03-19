using System.Collections.Generic;

namespace LiveHTS.Core.Model.SmartCard
{
    public class MOTHERDETAILS
    {
        public MOTHERNAME MOTHER_NAME { get; set; }
        public List<MOTHERIDENTIFIER> MOTHER_IDENTIFIER { get; set; }
    }
}