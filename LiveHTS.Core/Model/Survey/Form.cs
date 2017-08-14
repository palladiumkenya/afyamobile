using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class Form:Entity<Guid>
    {
        public string Name { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public decimal Rank { get; set; }
        [Indexed]
        public Guid ModuleId { get; set; }
        [Ignore]
        public List<Question> Questions { get; set; }=new List<Question>();

        [Ignore]
        public Guid DefaultEncounterTypeId { get; set; }

        [Ignore]
        public List<Encounter> ClientEncounters { get; set; }=new List<Encounter>();



        public Form()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Display}";
        }
    }
}