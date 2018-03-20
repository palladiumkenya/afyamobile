using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.SmartCard;
using LiveHTS.Core.Model.SmartCard;

namespace LiveHTS.Infrastructure.Repository.SmartCard
{
    public class ClientShrRecordRepository : BaseRepository<ClientShrRecord, Guid>, IClientShrRecordRepository
    {
        public ClientShrRecordRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public void SaveOrUpdate(ClientShrRecord clientShrRecord)
        {
            _db.Execute($"DELETE FROM {nameof(ClientShrRecord)} WHERE ClientId=?", clientShrRecord.ClientId.ToString());
            Save(clientShrRecord);
        }

        public ClientShrRecord GetByClientId(Guid clientId)
        {
            var shr = _db.Table<ClientShrRecord>().FirstOrDefault(x => x.ClientId == clientId);
            return shr;
        }
    }
}