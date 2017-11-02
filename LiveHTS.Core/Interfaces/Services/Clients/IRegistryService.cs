using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IRegistryService
    {
        Client Load(Guid id);
        Client Find(Guid id);
        IEnumerable<Client> GetAllClients(string search="");
        void Save(Client client);
        void UpdateRelationShips(string relationshipTypeId,Guid clientId, Guid otherClientId);
        void SaveOrUpdate(Client client);
        void Delete(Guid clientId);
    }
}