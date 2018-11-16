using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository.Subject
{
    public interface IClientSummaryRepository : IRepository<ClientSummary, Guid>
    {
        IEnumerable<ClientSummary> GetByClientId(Guid clientId);

        void SaveSummary(ClientSummary clientState);
        void SaveSummary(List<ClientSummary> clientStates);

        void DeleteSummary(Guid clientId);
    }
}