using System;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class PersonAddress : Entity<Guid>, IPersonAddress
    {
        public string Landmark { get; set; }
        [Indexed]
        public int? CountyId { get; set; }
        public bool Preferred { get; set; }
        [Indexed]
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public Guid PersonId { get; set; }

        public PersonAddress()
        {
            Id = LiveGuid.NewGuid();
        }

        private PersonAddress(string landmark, int? countyId, bool preferred, decimal? lat, decimal? lng, Guid personId)
        {
            Landmark = landmark;
            CountyId = countyId;
            Preferred = preferred;
            Lat = lat;
            Lng = lng;
            PersonId = personId;
        }

        public static PersonAddress Create(string landmark, int? countyId, bool preferred, decimal? lat, decimal? lng, Guid personId)
        {
            return new PersonAddress(landmark,countyId,preferred,lat,lng,personId);
        }
    }
}