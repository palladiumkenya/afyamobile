using System;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Repository.Subject
{
    public interface IProviderRepository : IRepository<Provider,Guid>
    {
        Provider GetDefaultProvider();
    }
}