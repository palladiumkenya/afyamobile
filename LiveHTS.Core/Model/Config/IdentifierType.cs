﻿using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class IdentifierType : Entity<string>
    {
        public string Name { get; set; }

        public IdentifierType()
        {
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}