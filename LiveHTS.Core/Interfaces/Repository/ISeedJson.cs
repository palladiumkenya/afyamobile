using System.Collections.Generic;

namespace LiveHTS.Core.Interfaces.Repository
{
    public interface ISeedJson<T>
    {        
        List<T> Read();
    }
}