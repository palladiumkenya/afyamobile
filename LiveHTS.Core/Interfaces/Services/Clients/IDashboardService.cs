using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IDashboardService
    {
        Client LoadClient(Guid clientId);
        IEnumerable<Form> LoadForms(Guid? moduleId=null);
        void RemoveRelationShip(Guid id);
    }
}