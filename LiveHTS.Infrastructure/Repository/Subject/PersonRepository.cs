using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class PersonRepository:BaseRepository<Person,Guid>, IPersonRepository
    {
        public PersonRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public override void Save(Person entity)
        {
            base.Save(entity);
            _db.CreateTable<PersonContact>();
            _db.CreateTable<PersonAddress>();

            if (entity.Addresses.Any())
            {
                var addresses = entity.Addresses.ToList();
                _db.InsertAll(addresses);
            }


            if (entity.Contacts.Any())
            {
                var contacts = entity.Contacts.ToList();
                _db.InsertAll(contacts);
            }
                
        }

        public override void InsertOrUpdate(Person entity)
        {
            base.InsertOrUpdate(entity);

            _db.CreateTable<PersonContact>();
            _db.CreateTable<PersonAddress>();

            if (entity.Addresses.Any())
            {
                var addresses = entity.Addresses.ToList();
                foreach (var a in addresses)
                {
                    InsertOrUpdateAny(a);
                }
            }


            if (entity.Contacts.Any())
            {
                var contacts = entity.Contacts.ToList();
                foreach (var a in contacts)
                {
                    InsertOrUpdateAny(a);
                }
            }
        }

        public override void Delete(Guid id)
        {
            _db.Execute($"DELETE FROM {nameof(PersonAddress)} WHERE PersonId=?", id.ToString());
            _db.Execute($"DELETE FROM {nameof(PersonContact)} WHERE PersonId=?", id.ToString());
            _db.Execute($"DELETE FROM {nameof(Person)} WHERE Id=?", id.ToString());
        }

        public void Sync(List<Person> providers)
        {
            throw new NotImplementedException();
        }

        public void Purge(Guid id)
        {
            _db.Execute($"DELETE FROM {nameof(PersonAddress)} WHERE PersonId IN (SELECT PersonId FROM {nameof(Client)} WHERE Id=?)", id.ToString());
            _db.Execute($"DELETE FROM {nameof(PersonContact)} WHERE PersonId IN (SELECT PersonId FROM {nameof(Client)} WHERE Id=?)", id.ToString());
            _db.Execute($"DELETE FROM {nameof(Person)} WHERE Id IN (SELECT PersonId FROM {nameof(Client)} WHERE Id=?)", id.ToString());
        }
    }
}