using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IObsService
    {
        Manifest Manifest { get; }
        Response Response { get; }
        
        void Initialize(Encounter encounter);
        List<SetResponse> GetTransformationComplexActions(Manifest currentManifest, Guid currentQuestionId);
        List<SetResponse> GetTransformationActions(Manifest currentManifest, Guid currentQuestionId);
        List<SetResponse> GetREmoteTransformationActions(Manifest currentManifest, Guid currentQuestionId);
        Question GetLiveQuestion(Manifest manifest=null);
        Question GetLiveQuestion(Manifest manifest, Guid currentQuestionId);
        List<Question> GetLiveQuestions(Manifest manifest, Guid currentQuestionId);
        Question GetNextQuestion(Guid currentQuestionId, Manifest manifest = null);
        Question GetPreviousQuestion(Guid currentQuestionId);
        Question GetQuestion(Guid questionId, Manifest currentManifest);

        List<Obs> GetObs(Guid clientId, Guid questionId);
        
        bool ValidateResponse(Guid encounterId, Guid clientId, Guid questionId, object response);
        void SaveResponse(Guid encounterId, Guid clientId, Guid questionId, object response,bool validated=false);
        void SaveClientResponse(Guid cientId, Guid questionId, object response);

        void ClearEncounter(Guid encounterId);
        void MarkEncounterCompleted(Guid encounterId,Guid userId,bool completed);
        void UpdateEncounterDate(Guid id, DateTime encounterDate,VisitType visitType);
    }
}