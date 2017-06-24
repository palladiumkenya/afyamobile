using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Infrastructure.Repository
{
    public abstract class BaseRepository<T>: IRepository<T> where T:Entity
    {
        internal List<T> _entities;

        protected BaseRepository()
        {
            _entities=new List<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _entities;
        }

        public virtual void Save(T entity)
        {
            _entities.Add(entity);
        }
    }
}