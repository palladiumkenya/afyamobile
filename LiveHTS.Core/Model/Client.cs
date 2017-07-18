using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model
{
    public class Client:Entity<Guid>
    {
        public IEnumerable<ClientIdentifier> Identifiers { get; set; }
        public IEnumerable<ClientRelationship> Relationships { get; set; }
        [Indexed]
        public Guid PracticeId { get; set; }
        [Indexed]
        public Guid PersonId { get; set; }
    }
}