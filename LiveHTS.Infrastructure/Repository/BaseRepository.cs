using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public T Get(Guid id)
        {
            return _entities
                .FirstOrDefault(x => x.Id == id);
        }
        public virtual IEnumerable<T> GetAllBy(Predicate<T> predicate)
        {
            return _entities.FindAll(predicate).ToList();
        }


        public virtual void Save(T entity)
        {
            _entities.Add(entity);
        }
    }
}