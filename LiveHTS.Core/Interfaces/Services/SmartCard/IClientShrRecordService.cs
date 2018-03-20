using System;
using LiveHTS.Core.Model.SmartCard;

namespace LiveHTS.Core.Interfaces.Services.SmartCard
{
    public interface IClientShrRecordService
    {
        void SaveOrUpdate(ClientShrRecord clientShrRecord);
        ClientShrRecord GetByClientId(Guid clientId);
    }
}