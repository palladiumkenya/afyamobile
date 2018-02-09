using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class UserRepository:BaseRepository<User,Guid>,IUserRepository
    {
        public UserRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
            
        }

        public override User Get(Guid id, bool voided = false)
        {
            var User =base.Get(id);
            if (null != User)
            {

             User.Person= _db.Table<Person>().FirstOrDefault(x => x.Id == User.PersonId);
            }
            return User;
        }

        public void Sync(List<User> providers)
        {
            throw new NotImplementedException();
        }
    }
}