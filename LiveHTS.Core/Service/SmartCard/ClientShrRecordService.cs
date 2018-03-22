using System;
using LiveHTS.Core.Interfaces.Repository.SmartCard;
using LiveHTS.Core.Interfaces.Services.SmartCard;
using LiveHTS.Core.Model.SmartCard;

namespace LiveHTS.Core.Service.SmartCard
{
    public class ClientShrRecordService: IClientShrRecordService
    {
        private readonly IClientShrRecordRepository _clientShrRecordRepository;

        public ClientShrRecordService(IClientShrRecordRepository clientShrRecordRepository)
        {
            _clientShrRecordRepository = clientShrRecordRepository;
        }

        public void SaveOrUpdate(ClientShrRecord clientShrRecord)
        {
            _clientShrRecordRepository.Save(clientShrRecord);
        }

        public ClientShrRecord GetByClientId(Guid clientId)
        {
            return _clientShrRecordRepository.GetByClientId(clientId);
        }
    }
}