using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IRemoteSearchService
    {
        IEnumerable<Client> GetAllClients(string search = "");
    }
}