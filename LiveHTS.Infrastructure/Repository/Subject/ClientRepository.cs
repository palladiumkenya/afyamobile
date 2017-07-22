using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public override IEnumerable<Client> GetAll(Expression<Func<Client, bool>> predicate, bool voided = false)
        {
            var clients = base.GetAll(predicate, voided).ToList();

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
            var cIds = _db.Table<ClientIdentifier>().Where(x => x.Identifier.ToLower().Contains(search.ToLower()))
                .Select(x => x.ClientId)
                .ToList();
            var pIds = _db.Table<Person>().Where(
                    x => x.FirstName.ToLower().Contains(search.ToLower()) ||
                         x.MiddleName.ToLower().Contains(search.ToLower()) ||
                         x.LastName.ToLower().Contains(search.ToLower())
                )
                .Select(x => x.Id)
                .ToList();

            var clients = _db.Table<Client>();
            if (cIds.Count>0)
            {
                clients = _db.Table<Client>().Where(x=>cIds.Contains(x.Id));
            }
            if (pIds.Count > 0)
            {
                clients = _db.Table<Client>().Where(x => cIds.Contains(x.Id));
            }

            return clients;
        }
    }
}