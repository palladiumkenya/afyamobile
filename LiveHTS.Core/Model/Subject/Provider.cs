using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class Provider:Entity<Guid>
    {
        [Indexed]
        public string ProviderTypeId { get; set; }
        public string Code { get; set; }
        [Indexed]
        public Guid PracticeId { get; set; }
        [Indexed]
        public Guid PersonId { get; set; }
        [Ignore]
        public Person Person { get; set; }
        public Provider()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}