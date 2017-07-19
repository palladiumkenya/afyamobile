using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services
{
    public interface IInterviewService
    {
        Manifest Manifest { get; } 
        Question LiveQuestion { get; }

        void Open(Guid formId, Guid encounterTypeId, Guid clientId, Guid practiceId);
        void Start(Guid practiceId, Guid deviceId, Guid providerId, Guid userId);
        void Resume();
        void Discard();
    }
}