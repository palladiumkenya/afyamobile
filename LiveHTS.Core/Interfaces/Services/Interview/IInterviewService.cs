using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Interfaces.Services.Interview
{
    public interface IInterviewService
    {
        IEnumerable<Encounter> LoadEncounters(Guid clientId, Guid formId);
        IEnumerable<Encounter> LoadEncounters(Guid clientId, Guid formId, Guid indexClient);
        IEnumerable<Encounter> LoadKeyEncounters(Guid clientId);
    }
}