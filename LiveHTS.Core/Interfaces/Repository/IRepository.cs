using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository
{
    public interface IRepository<T> where T:Entity
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllBy(Predicate<T> predicate);
        void Save(T entity);
    }
}