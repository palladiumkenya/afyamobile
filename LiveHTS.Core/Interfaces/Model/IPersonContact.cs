using System;

namespace LiveHTS.Core.Interfaces.Model
{
    public interface IPersonContact
    {
        string Phone { get; set; }
        bool Preferred { get; set; }
    }
}
