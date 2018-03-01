using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Engine;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class ClientSummaryRepository : BaseRepository<ClientSummary, Guid>, IClientSummaryRepository
    {
        public ClientSummaryRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
        public IEnumerable<ClientSummary> GetByClientId(Guid clientId)
        {
            var states=GetAll(x => x.ClientId == clientId).ToList();
            return states;
        }

        public void SaveSummary(ClientSummary clientState)
        {
           InsertOrUpdate(clientState);
        }

        public void SaveSummary(List<ClientSummary> clientStates)
        {
            foreach (var clientState in clientStates)
            {
                SaveSummary(clientState);
            }
        }

        public void DeleteSummary(Guid clientId)
        {
            _db.Execute($"DELETE FROM {nameof(ClientSummary)} WHERE ClientId=?", clientId.ToString());
        }

    }
}