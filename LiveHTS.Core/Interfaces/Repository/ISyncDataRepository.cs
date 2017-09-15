using System.Collections.Generic;

namespace LiveHTS.Core.Interfaces.Repository
{
    public interface ISyncDataRepository
    {
        void Update<T>(T data) where T : class;
        void Update<T>(List<T> data) where T : class;
    }
}