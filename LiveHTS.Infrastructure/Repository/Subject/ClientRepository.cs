using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Interview;
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
                client.ClientStates= _db.Table<ClientState>().Where(x => x.ClientId == id).ToList();

                client.Person = _db.Table<Person>().FirstOrDefault(x => x.Id == client.PersonId);
                var contacts = _db.Table<PersonContact>().ToList();
                client.Person.Contacts = _db.Table<PersonContact>().Where(x => x.PersonId == client.PersonId).ToList();
                client.Person.Addresses = _db.Table<PersonAddress>().Where(x => x.PersonId == client.PersonId).ToList();

                var relationsList=new List<ClientRelationship>();
                
                //my Relationships
                var relations = _db.Table<ClientRelationship>().Where(x => x.ClientId == client.Id).ToList();
                if (null != relations && relations.Count > 0)
                {
                    client.MyRelationships = relations;
                    relationsList.AddRange(relations);
                }

                //related to
                var relationsTo = _db.Table<ClientRelationship>().Where(x => x.ClientId != client.Id && x.RelatedClientId == client.Id).ToList();
                if (null != relationsTo && relationsTo.Count > 0)
                {
                    client.RelatedToMe = relationsTo;
                    relationsList.AddRange(relationsTo);
                }
                    

                client.Relationships = relationsList;

                client.Identifiers = _db.Table<ClientIdentifier>().Where(x => x.ClientId == client.Id).ToList(); ;
            }
            return client;
        }

        public override IEnumerable<Client> GetAll(bool voided = false)
        {
            var clients = _db.Table<Client>().ToList();

            foreach (var c in clients)
            {
                c.ClientStates = _db.Table<ClientState>().Where(x => x.ClientId == c.Id).ToList();
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
                c.ClientStates = _db.Table<ClientState>().Where(x => x.ClientId == c.Id).ToList();
                c.Person = _db.Table<Person>().FirstOrDefault(x => x.Id == c.PersonId);
                c.Relationships = _db.Table<ClientRelationship>().Where(x => x.ClientId == c.Id).ToList();
                c.Identifiers = _db.Table<ClientIdentifier>().Where(x => x.ClientId == c.Id).ToList();
            }
            return clients;
        }

        public override void Save(Client entity)
        {
            base.Save(entity);


            //Create identifiers
            if (entity.Identifiers.Any())
            {
                var identifiers = entity.Identifiers.ToList();
                _db.InsertAll(identifiers);
            }                

            return;
            //TODO: Create without Relationships index
            if (entity.Relationships.Any())
            {
                var relationships = entity.Relationships.ToList();
                _db.InsertAll(relationships);
            }                
        }

        public override void InsertOrUpdate(Client entity)
        {
            base.InsertOrUpdate(entity);
       

            //Create identifiers
            if (entity.Identifiers.Any())
            {
                var identifiers = entity.Identifiers.ToList();
                foreach (var a in identifiers)
                {
                    InsertOrUpdateAny(a);
                }
            }

            
            return;
            //TODO: Create without Relationships index
            if (entity.Relationships.Any())
            {
                var relationships = entity.Relationships.ToList();
                foreach (var a in relationships)
                {
                    InsertOrUpdateAny(a);
                }
            }
        }

        public Client Load(Guid id)
        {
            var client = base.Get(id);
            if (null != client)
            {
                client.Person = _db.Table<Person>().FirstOrDefault(x => x.Id == client.PersonId);
                client.Identifiers = _db.Table<ClientIdentifier>().Where(x => x.ClientId == client.Id).ToList(); 
            }
            return client;
        }

        public IEnumerable<Guid> GetAllClientIds()
        {
            var clients = _db.Table<Client>().ToList().Select(x => x.Id).ToList();
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

        public void Purge(Guid id)
        {
            _db.Execute($"DELETE FROM {nameof(ClientState)} WHERE ClientId=?", id.ToString());
            _db.Execute($"DELETE FROM {nameof(ClientIdentifier)} WHERE ClientId=?", id.ToString());
            _db.Execute($"DELETE FROM {nameof(Client)} WHERE Id=?", id.ToString());
        }

        private void DeleteClientData(Guid id)
        {
            var personIds = _db.Table<Client>().Where(x => x.Id == id).Select(x => x.PersonId).ToList();
            var encounterIds = _db.Table<Encounter>().Where(x => x.ClientId == id).Select(x => x.Id).ToList();

            //Encounter

            foreach (var encounterId in encounterIds)
            {
                _db.Table<Obs>().Delete(x => x.EncounterId == encounterId);
                _db.Table<ObsTestResult>().Delete(x => x.EncounterId == encounterId);
                _db.Table<ObsFinalTestResult>().Delete(x => x.EncounterId == encounterId);
                _db.Table<ObsLinkage>().Delete(x => x.EncounterId == encounterId);
                _db.Table<ObsMemberScreening>().Delete(x => x.EncounterId == encounterId);
                _db.Table<ObsFamilyTraceResult>().Delete(x => x.EncounterId == encounterId);
                _db.Table<ObsPartnerScreening>().Delete(x => x.EncounterId == encounterId);
                _db.Table<ObsPartnerTraceResult>().Delete(x => x.EncounterId == encounterId); 
            }

            //Client

            _db.Table<ClientRelationship>().Delete(x => x.ClientId == id);
            _db.Table<ClientIdentifier>().Delete(x => x.ClientId ==id);
            _db.Table<ClientState>().Delete(x => x.ClientId == id);
            //Person

            foreach (var personId in personIds)
            {
                _db.Table<PersonAddress>().Delete(x => x.PersonId == personId);
                _db.Table<PersonContact>().Delete(x => x.PersonId == personId);
                _db.Table<Person>().Delete(x => x.Id == personId);
            }
        }
    }
}