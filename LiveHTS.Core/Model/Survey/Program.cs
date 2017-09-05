using System;
using LiveHTS.Core.Model.Config;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class Program:Entity<Guid>
    {
        [Indexed]
        public Guid FormId { get; set; }
        [Indexed]
        public Guid EncounterTypeId { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public Decimal Rank { get; set; }
    }
}