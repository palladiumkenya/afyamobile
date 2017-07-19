using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services
{
    public interface IInterviewService
    {
        Encounter LoadEncounter(Guid formId, Guid encounterTypeId, Guid clientId);
        Encounter StartEncounter(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId);
        Encounter OpenEncounter(Guid encounterId);
    }
}