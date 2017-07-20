using System;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services.Clients
{
    public interface IObsService
    {
        Manifest Manifest { get; }
        Response Response { get; }

        void Initialize();
        Question GetLiveQuestion();
        Question GetNextQuestion(Guid currentQuestionId);
        Question GetPreviousQuestion(Guid currentQuestionId);
        void SaveResponse(Guid encounterId, Guid questionId, object response);
    }
}