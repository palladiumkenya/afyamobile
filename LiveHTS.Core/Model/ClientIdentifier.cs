using System;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model
{
    public class ClientIdentifier : Entity<Guid>
    {
        [Indexed]
        public string IdentifierTypeId { get; set; }
        public string Identifier { get; set; }
        public bool Preferred { get; set; }
        [Indexed]
        public Guid ClientId { get; set; }
    }
}