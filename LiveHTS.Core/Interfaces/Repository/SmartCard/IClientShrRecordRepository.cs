using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository.SmartCard
{
    public interface IClientShrRecordRepository : IRepository<ClientShrRecord, Guid>
    {
        void SaveOrUpdate(ClientShrRecord clientShrRecord);
        ClientShrRecord GetByClientId(Guid clientId);
    }
}