using System;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using Newtonsoft.Json;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class PersonAddress : Entity<Guid>, IPersonAddress
    {
        public int? WardId { get; set; }
        public string Landmark { get; set; }

        [Indexed]
        public int? CountyId { get; set; }

        public int? SubCountyId { get; set; }
        public bool Preferred { get; set; }

        [Indexed]
        [JsonIgnore]
        public decimal? Lat { get; set; }

        [JsonIgnore]
        public decimal? Lng { get; set; }

        public Guid PersonId { get; set; }

        public PersonAddress()
        {
            Id = LiveGuid.NewGuid();
        }

        private PersonAddress(string landmark, int? countyId, bool preferred, decimal? lat, decimal? lng, Guid personId,
            int? subCountyId, int? wardId) : this()
        {
            Landmark = landmark;
            CountyId = countyId;
            Preferred = preferred;
            Lat = lat;
            Lng = lng;
            PersonId = personId;
            SubCountyId = subCountyId;
            WardId = wardId;
        }

        public static PersonAddress Create(string landmark, int? countyId, bool preferred, decimal? lat, decimal? lng,
            Guid personId, int? subCountyId, int? wardId)
        {
            return new PersonAddress(landmark, countyId, preferred, lat, lng, personId, subCountyId, wardId);
        }
    }
}