using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.SharedKernel.Model;
using SQLite;


namespace LiveHTS.Infrastructure.Repository
{
    public abstract class BaseRepository<T,TId>:IRepository<T,TId> where T : Entity<TId>,new()
    {
        private readonly string _databasePath;
        protected SQLiteConnection _db;

        protected BaseRepository(string databasePath)
        {
            _databasePath = databasePath;
            _db = new SQLiteConnection(_databasePath);
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