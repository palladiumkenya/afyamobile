using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IClientReaderService
    {
        Client LoadClient(Guid clientId);
        List<Guid> LoadClientIds();
        List<Encounter> LoadEncounters(Guid clientId);
    }
}