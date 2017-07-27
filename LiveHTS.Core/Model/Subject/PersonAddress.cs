using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class PersonAddress : Entity<Guid>
    {
        public string Landmark { get; set; }
        [Indexed]
        public Guid? CountyId { get; set; }
        public bool Preferred { get; set; }
        [Indexed]
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public Guid PersonId { get; set; }

        public PersonAddress()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}