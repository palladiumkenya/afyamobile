using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.SharedKernel.Model;
using SQLite;


namespace LiveHTS.Infrastructure.Repository
{
    public abstract class MetaRepository<T, TId> : IMetaRepository<T, TId> where T : Entity<TId>, new()
    {
        private readonly ILiveSetting _liveSetting;
        protected SQLiteConnection _db;

        protected MetaRepository(ILiveSetting liveSetting)
        {
            _liveSetting = liveSetting;
            _db = new SQLiteConnection(_liveSetting.MetaDatabasePath);
            
            _db.CreateTable<T>();
        }

        public virtual IEnumerable<T> GetAll(bool voided = false)
        {
            var results = _db.Table<T>()
                .Where(x => x.Voided == voided);

            return results;
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _db.Table<T>()
                .Where(predicate);
        }

        public virtual void InsertOrUpdate(T entity)
        {
            var rowsAffected = _db.Update(entity);
            if (rowsAffected == 0)
            {
                _db.Insert(entity);
            }
        }
    }
}