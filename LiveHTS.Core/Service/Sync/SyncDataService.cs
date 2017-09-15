using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Interfaces.Services.Sync;
using SQLite;

namespace LiveHTS.Core.Service.Sync
{
    public class SyncDataService:ISyncDataService
    {
        private ISyncDataRepository _syncDataRepository;

        public SyncDataService(ISyncDataRepository syncDataRepository)
        {
            _syncDataRepository = syncDataRepository;
        }


        public void Update<T>(T data) where T : class
        {
            _syncDataRepository.Update(data);
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
