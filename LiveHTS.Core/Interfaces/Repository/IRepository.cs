using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository
{
    public interface IRepository<T,TId> where T:Entity<TId>
    {
        T Get(TId id);
        IEnumerable<T> GetAll();
        void Save(T entity);
        void Update(T entity);
        void Delete(TId id);
    }
}