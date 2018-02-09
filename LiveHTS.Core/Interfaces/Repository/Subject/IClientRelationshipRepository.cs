using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Repository.Subject
{
    public interface IClientRelationshipRepository : IRepository<ClientRelationship,Guid>
    {
        IEnumerable<ClientRelationship> GetRelationships(Guid clientId);
        ClientRelationship Find(string relationshipTypeId, Guid clientId, Guid otherClientId);
        void Purge(Guid clientId);
        void PurgeRel(Guid guid);
    }
}