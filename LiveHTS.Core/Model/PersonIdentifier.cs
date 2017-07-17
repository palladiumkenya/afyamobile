using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model
{
    public class PersonIdentifier:Entity<Guid>
    {
        public string IdentifierTypeId { get; set; }
        public string Identifier { get; set; }
    }
}