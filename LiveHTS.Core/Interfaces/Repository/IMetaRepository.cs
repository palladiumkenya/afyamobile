using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository
{
    public interface IMetaRepository<T, TId> where T : Entity<TId>
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
    }
}