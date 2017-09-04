using System;

namespace LiveHTS.Core.Interfaces.Model
{
    public interface IPersonAddress
    {
        int? CountyId { get; set; }
        string Landmark { get; set; }
        decimal? Lat { get; set; }
        decimal? Lng { get; set; }
        bool Preferred { get; set; }
    }
}