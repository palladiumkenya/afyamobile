using System;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Config
{
    public class Device:Entity<Guid>
    {
        public string Serial { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Indexed]
        public Guid PracticeId { get; set; }
    }
}