using System.Collections.Generic;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface ISyncDataService
    {
        void Update<T>(T data) where T : class;
        void Update<T>(List<T> data) where T : class;
    }
}