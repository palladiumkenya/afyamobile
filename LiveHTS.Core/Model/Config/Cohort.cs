using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class Cohort:Entity<Guid>
    {
        public string Name { get; set; }
        public string Display { get; set; }
        public int? Count { get; set; }
        public int Rank { get; set; }
        public DateTime? Updated { get; set; }

        public Cohort()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Display}";
        }
    }
}