using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Repository.Subject
{
    public interface IUserRepository:IRepository<User,Guid>
    {
        void Sync(List<User> providers);
    }
}