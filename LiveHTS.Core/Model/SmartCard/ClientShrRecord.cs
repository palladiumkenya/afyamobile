using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.SmartCard
{
    public class ClientShrRecord:Entity<Guid>
    {
        public Guid  ClientId { get; set; }
        public string Shr { get; set; }

        public ClientShrRecord()
        {
        }

        public ClientShrRecord(Guid clientId, string shr)
        {
            ClientId = clientId;
            Shr = shr;
        }
    }
}