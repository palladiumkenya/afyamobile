using System;

namespace LiveHTS.Core.Interfaces.Model
{
    public interface IPersonAddress
    {
        Guid? CountyId { get; set; }
        string Landmark { get; set; }
        decimal? Lat { get; set; }
        decimal? Lng { get; set; }
        Guid PersonId { get; set; }
        bool Preferred { get; set; }
    }
}