using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model
{
    public class ClientRelationship : Entity<Guid>
    {
        [Indexed]
        public string RelationshipTypeId { get; set; }
        [Indexed]
        public Guid RelatedClientId { get; set; }
        public bool Preferred { get; set; }
        [Indexed]
        public Guid ClientId { get; set; }
    }
}