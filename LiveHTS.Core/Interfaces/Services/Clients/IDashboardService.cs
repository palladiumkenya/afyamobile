using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IDashboardService
    {
        Client LoadClient(Guid clientId);
        Module LoadModule();
        List<Module> LoadModules();
        void RemoveRelationShip(Guid id);
        void RemoveEncounter(Guid id);
    }
}