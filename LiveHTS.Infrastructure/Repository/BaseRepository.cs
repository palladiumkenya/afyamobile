using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.SharedKernel.Model;
using SQLite;


namespace LiveHTS.Infrastructure.Repository
{
    public abstract class BaseRepository<T,TId>:IRepository<T,TId> where T : Entity<TId>,new()
    {
        private readonly ILiveSetting _liveSetting;
        protected SQLiteConnection _db;

        protected BaseRepository(ILiveSetting liveSetting)
        {
            _liveSetting = liveSetting;
            _db = new SQLiteConnection(_liveSetting.DatasePath);
            _db.CreateTable<T>();
        }

        public virtual T Get(TId id)
        {
            return _db.Find<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _db.Table<T>();
        }

        public virtual void Save(T entity)
        {
            _db.Insert(entity);
        }

        public virtual void Update(T entity)
        {
            _db.Update(entity);
        }

        public virtual  void Delete(TId id)
        {
            _db.Delete<T>(id);
        }
    }
}