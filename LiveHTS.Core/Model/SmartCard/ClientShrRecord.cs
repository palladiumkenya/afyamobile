using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.SmartCard
{
    public class ClientShrRecord:Entity<Guid>
    {
        public Guid  ClientId { get; set; }
        public string Shr { get; set; }

        public ClientShrRecord()
        {
            Id = LiveGuid.NewGuid();
        }

        public ClientShrRecord(Guid clientId, string shr):this()
        {
            ClientId = clientId;
            Shr = shr;
        }
    }
}