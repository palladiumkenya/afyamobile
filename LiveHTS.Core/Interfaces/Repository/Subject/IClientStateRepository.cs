using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository.Subject
{
    public interface IClientStateRepository : IRepository<ClientState,Guid>
    {
        IEnumerable<ClientState> GetByClientId(Guid clientId);
        IEnumerable<ClientState> GetByClientId(Guid clientId,LiveState state);
        void SaveOrUpdate(ClientState clientState);
        void SaveOrUpdate(List<ClientState> clientStates);
        void DeleteState(Guid clientId, LiveState state);
        void DeleteState(Guid clientId,Guid encounterId,LiveState state);
    }
}