using System;

namespace LiveHTS.Core.Interfaces.Model
{
    public interface IPersonContact
    {
        long? Phone { get; set; }
        bool Preferred { get; set; }
    }
}
