using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository.SmartCard
{
    public interface IPSmartStoreRepository : IRepository<PSmartStore, Guid>
    {
        void SaveOrUpdate(PSmartStore pSmartStore);
        List<PSmartStore> LoadAll(Guid clientId);
    }
}