using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class RelationshipType:Entity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}