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
                _db.InsertAll(entity.Addresses);

            if(entity.Contacts.Any())
                _db.InsertAll(entity.Contacts);
        }
    }
}