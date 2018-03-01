using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IRegistryService
    {
        Client Load(Guid id);
        Client Find(Guid id);
        IEnumerable<Client> GetAllClients(string search="");
        IEnumerable<Client> GetAllCohortClients(Guid cohortId,string search = "");
        void Save(Client client);
        void Save(Client client,Guid cohortId);
        void UpdateRelationShips(string relationshipTypeId,Guid clientId, Guid otherClientId);
        void SaveOrUpdate(Client client,bool isClient=true);
        void SaveDownloaded(Client client);
        Task Download(Client client,List<Encounter> encounters);

        void Delete(Guid clientId);
    }
}