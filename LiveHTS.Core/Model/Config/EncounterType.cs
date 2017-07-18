using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class EncounterType : Entity<Guid>
    {
        public string Name { get; set; }

        public EncounterType()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}