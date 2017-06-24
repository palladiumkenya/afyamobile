using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository
{
    public interface IRepository<T> where T:Entity
    {
        IEnumerable<T> GetAll();
        void Save(T entity);
    }
}