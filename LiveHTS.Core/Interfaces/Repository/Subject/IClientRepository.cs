using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Repository.Subject
{
    public interface IClientRepository:IRepository<Client,Guid>
    {
        Client Load(Guid id);
        IEnumerable<Guid> GetAllClientIds();
        IEnumerable<Guid> GetAllClientIds(Guid pracId);
        void SaveOrUpdate(Client client);
        void SaveDownloaded(Client client);
        IEnumerable<Client> QuickSearch(string search);
        void Purge(Guid id);
        void ClearIncomplete();
        void MarkIncomplete(Guid id);
    }
}
