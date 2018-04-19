using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.SmartCard;
using LiveHTS.Core.Model.SmartCard;

namespace LiveHTS.Infrastructure.Repository.SmartCard
{
    public class PSmartStoreRepository: BaseRepository<PSmartStore, Guid>, IPSmartStoreRepository
    {
        public PSmartStoreRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public void SaveOrUpdate(PSmartStore pSmartStore)
        {
            var pSmartStores = GetAll(x => x.ClientId == pSmartStore.ClientId).ToList();
            if (pSmartStores.Count > 0)
            {
                //update
                var pstore = pSmartStores.First();
                pstore.UpdateShr(pSmartStore);
                Update(pstore);
            }
            else
            {
                Save(pSmartStore);
            }
        }

        public List<PSmartStore> LoadAll(Guid clientId)
        {
            return GetAll(x => x.ClientId == clientId).ToList();
        }
    }
}