using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository;
using SQLite;

namespace LiveHTS.Infrastructure.Repository
{
    public class SyncDataRepository :ISyncDataRepository
    {
        private readonly ILiveSetting _liveSetting;
        protected SQLiteConnection _db;

        protected SyncDataRepository(ILiveSetting liveSetting)
        {
            _liveSetting = liveSetting;
            _db = new SQLiteConnection(_liveSetting.DatasePath);
        }


        public void Update<T>(T data) where T : class
        {
            var rowsAffected = _db.Update(data);
            if (rowsAffected == 0)
            {
                _db.Insert(data);
            }
        }

        public void Update<T>(List<T> data) where T : class
        {
            foreach (var d in data)
            {
                 Update(d);
            }
        }
    }
}
