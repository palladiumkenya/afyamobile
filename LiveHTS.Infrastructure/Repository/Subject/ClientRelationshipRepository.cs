using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class ClientRelationshipRepository : BaseRepository<ClientRelationship,Guid>, IClientRelationshipRepository
    {
        public ClientRelationshipRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}