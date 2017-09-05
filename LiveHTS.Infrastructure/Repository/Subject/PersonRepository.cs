using System;
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
    }
}