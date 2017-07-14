using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository
{
    public interface IRepository<T,TId> where T:Entity<TId>
    {
        T Get(TId id, bool voided = false);
        IEnumerable<T> GetAll(bool voided = false);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, bool voided = false);
        void Save(T entity);
        void Update(T entity);
        void Delete(TId id);
        void Void(TId id);
    }
}