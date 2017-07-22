using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class ClientRepository:BaseRepository<Client,Guid>,IClientRepository
    {
        public ClientRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
            
        }

        public override Client Get(Guid id, bool voided = false)
        {
            var client = base.Get(id);
            if (null != client)
            {
                client.Person = _db.Table<Person>().FirstOrDefault(x => x.Id == client.PersonId);
                client.Relationships = _db.Table<ClientRelationship>().Where(x => x.ClientId == client.Id);
                client.Identifiers = _db.Table<ClientIdentifier>().Where(x => x.ClientId == client.Id);
            }
            return client;
        }

        public override IEnumerable<Client> GetAll(bool voided = false)
        {
            var clients = _db.Table<Client>().ToList();

            foreach (var c in clients)
            {
                c.Person = _db.Table<Person>().FirstOrDefault(x => x.Id == c.PersonId);
                c.Relationships = _db.Table<ClientRelationship>().Where(x => x.ClientId == c.Id).ToList();
                c.Identifiers = _db.Table<ClientIdentifier>().Where(x => x.ClientId == c.Id).ToList();
            }

            return clients;
        }

        public void SaveOrUpdate(Client obs)
        {
            var existingObs = _db.Find<Client>(obs.Id);

            if (null != existingObs)
            {
                Update(obs);
            }
            else
            {
                Save(obs);
            }
        }

        public IEnumerable<Client> QuickSearch(string search)
        {
            
            var clients = _db.Table<Client>().ToList();

            foreach (var c in clients)
            {
                c.Person = _db.Table<Person>().FirstOrDefault(x => x.Id == c.PersonId);
                c.Relationships = _db.Table<ClientRelationship>().Where(x => x.ClientId == c.Id).ToList();
                c.Identifiers = _db.Table<ClientIdentifier>().Where(x => x.ClientId == c.Id).ToList();
            }

            return clients;
        }
    }
}