using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Repository.Subject
{
    public interface IClientRelationshipRepository : IRepository<ClientRelationship,Guid>
    {
        IEnumerable<ClientRelationship> GetRelationships(Guid clientId);
    }
}