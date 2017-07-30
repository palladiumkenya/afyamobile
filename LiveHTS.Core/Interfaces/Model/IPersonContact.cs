using System;

namespace LiveHTS.Core.Interfaces.Model
{
    public interface IPersonContact
    {
        int? Phone { get; set; }
        bool Preferred { get; set; }
    }
}