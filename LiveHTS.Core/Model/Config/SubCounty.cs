using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Config
{
    public class SubCounty:Entity<Guid>
    {
        public string Name { get; set; }
        [Indexed]
        public int CountyId { get; set; }

        public SubCounty()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}