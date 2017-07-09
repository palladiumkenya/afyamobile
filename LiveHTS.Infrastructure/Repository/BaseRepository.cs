using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Infrastructure.Repository
{
    public abstract class BaseRepository<T,TId>:IRepository<T,TId> where T : Entity<TId>,new()
    {
        private SQLiteConnection db;

        protected BaseRepository()
        {
            db=new SQLiteConnection("livehts");
            db.CreateTable<T>();
        }

        protected BaseRepository(SQLiteConnection db)
        {
            this.db = db;
        }

        public T Get(TId id)
        {
            return db.Find<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return db.Table<T>();
        }

        public void Save(T entity)
        {
            db.Insert(entity);
        }

        public void Update(T entity)
        {
            db.Update(entity);
        }

        public void Delete(TId id)
        {
            db.Delete<T>(id);
        }
    }
}