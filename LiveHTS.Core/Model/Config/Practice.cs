using System;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Config
{
    public class Practice : Entity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        [Indexed]
        public string PracticeTypeId { get; set; }
        [Indexed]
        public int? CountyId { get; set; }

        public Practice()
        {
        }

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }
    }
}