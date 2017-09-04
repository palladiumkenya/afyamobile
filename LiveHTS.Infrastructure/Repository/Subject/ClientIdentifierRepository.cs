using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class ClientIdentifierRepository : BaseRepository<ClientIdentifier,Guid>, IClientIdentifierRepository
    {
        public ClientIdentifierRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}