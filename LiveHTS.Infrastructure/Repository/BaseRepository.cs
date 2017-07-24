using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.SharedKernel.Model;
using SQLite;


namespace LiveHTS.Infrastructure.Repository
{
    public abstract class BaseRepository<T, TId> : IRepository<T, TId> where T : Entity<TId>, new()
    {
        private readonly ILiveSetting _liveSetting;
        protected SQLiteConnection _db;

        protected BaseRepository(ILiveSetting liveSetting)
        {
            _liveSetting = liveSetting;
            _db = new SQLiteConnection(_liveSetting.DatasePath);
            
            _db.CreateTable<T>();
        }

        public virtual T Get(TId id, bool voided = false)
        {
            var entity = _db.Find<T>(id);
            if (null == entity)
                return null;

            return entity.Voided == voided ? entity : null;
        }

        public virtual IEnumerable<T> GetAll(bool voided = false)
        {
            var results= _db.Table<T>()
                .Where(x => x.Voided == voided);

            return results;
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, bool voided = false)
        {
            var results= _db.Table<T>()
                .Where(predicate);

            return results.Where(x=>x.Voided==voided);
        }

        public virtual void Save(T entity)
        {
            _db.Insert(entity);
        }

        public virtual void Update(T entity)
        {
            _db.Update(entity);
        }

        public virtual void Delete(TId id)
        {
            _db.Delete<T>(id);
        }

        public virtual void Void(TId id)
        {
            var entity = Get(id);
            if (null != entity)
            {
                entity.Voided = true;
                _db.Update(entity);
            }
        }
    }
}