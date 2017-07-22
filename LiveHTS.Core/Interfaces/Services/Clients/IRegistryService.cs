using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IRegistryService
    {
        IEnumerable<Client> GetAllClients(string search="");
        void SaveOrUpdate(Client client);
        void Delete(Guid clientId);
    }
}