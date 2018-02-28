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
    public class ClientStateRepository: BaseRepository<ClientState, Guid>, IClientStateRepository
    {
        public ClientStateRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
        public IEnumerable<ClientState> GetByClientId(Guid clientId, Guid? encounterId = null, LiveState? state = null, Guid? indexClientId = null)
        {
            var states=GetAll(x => x.ClientId == clientId).ToList();

            if (null != encounterId && !encounterId.IsNullOrEmpty())
                states = states.Where(x => x.EncounterId == encounterId.Value).ToList();

            if (null != state)
                states = states.Where(x => x.Status == state.Value).ToList();

            if (null != indexClientId && !indexClientId.IsNullOrEmpty())
                states = states.Where(x => x.IndexClientId == indexClientId.Value).ToList();

            return states;
        }

        public void SaveOrUpdate(ClientState clientState)
        {
            var states = GetByClientId(clientState.ClientId, clientState.EncounterId, clientState.Status, clientState.IndexClientId).ToList();

            if (states.Count == 0)
            {
                Save(clientState);
                return;
            }

            if (clientState.Status.CanBeMutliple())
                SaveOrUpdate(clientState);
        }

        public void SaveOrUpdate(List<ClientState> clientStates)
        {
            foreach (var clientState in clientStates)
            {
                SaveOrUpdate(clientState);
            }
        }

        public void DeleteState(Guid clientId, Guid? encounterId = null, LiveState? state = null, Guid? indexClientId = null)
        {
            var states = GetByClientId(clientId, encounterId, state).ToList();
            foreach (var clientState in states)
            {
                Delete(clientState.Id);
            }
        }

    }
}