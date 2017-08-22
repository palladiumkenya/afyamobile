using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IObsService
    {
        Manifest Manifest { get; }
        Response Response { get; }

        void Initialize(Encounter encounter);
        List<SetResponse> GetTransformationComplexActions(Manifest currentManifest, Guid currentQuestionId);
        List<SetResponse> GetTransformationActions(Manifest currentManifest, Guid currentQuestionId);
        Question GetLiveQuestion(Manifest manifest=null);
        Question GetLiveQuestion(Manifest manifest, Guid currentQuestionId);
        List<Question> GetLiveQuestions(Manifest manifest, Guid currentQuestionId);
        Question GetNextQuestion(Guid currentQuestionId, Manifest manifest = null);
        Question GetPreviousQuestion(Guid currentQuestionId);
        Question GetQuestion(Guid questionId, Manifest currentManifest);

        bool ValidateResponse(Guid encounterId, Guid questionId, object response);
        void SaveResponse(Guid encounterId, Guid questionId, object response,bool validated=false);
        void ClearEncounter(Guid encounterId);
    }
}