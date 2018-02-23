using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Engine;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class ClientStateRepository: BaseRepository<ClientState, Guid>, IClientStateRepository
    {
        public ClientStateRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public IEnumerable<ClientState> GetByClientId(Guid clientId)
        {
            return GetAll(x => x.ClientId == clientId).ToList();
        }

        public IEnumerable<ClientState> GetByClientId(Guid clientId, LiveState state)
        {
            return GetAll(x => x.ClientId == clientId && x.Status == state).ToList();
        }

        public void SaveOrUpdate(ClientState clientState)
        {
            var states = GetByClientId(clientState.ClientId, clientState.Status).ToList();

            if (states.Count == 0)
            {
                Save(clientState);
                return;
            }

            if(clientState.Status.CanBeMutliple())
                SaveOrUpdate(clientState);


        }

        public void SaveOrUpdate(List<ClientState> clientStates)
        {
            foreach (var clientState in clientStates)
            {
                SaveOrUpdate(clientState);
            }
        }

        public void DeleteByEncounterId(Guid encounterId)
        {
            _db.Execute($"DELETE FROM {nameof(ClientState)} WHERE EncounterId=?", encounterId.ToString());
        }
    }
}