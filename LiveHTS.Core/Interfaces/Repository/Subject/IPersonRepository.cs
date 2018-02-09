using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Repository.Subject
{
    public interface IPersonRepository:IRepository<Person,Guid>
    {
        void Sync(List<Person> providers);
        void Purge(Guid id);

    }
}