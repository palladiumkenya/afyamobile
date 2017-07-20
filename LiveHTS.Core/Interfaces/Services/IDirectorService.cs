using System;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services
{
    public interface IDirectorService
    {
        Manifest Manifest { get; }
        Response LiveResponse { get; }

        void Initialize();
        void UpdateManifest();
        Question GetLiveQuestion();
        void SaveResponse(Guid encounterId, Guid questionId,object response);
        bool ValidateResponse(Response response);

        Question GetNextQuestion();
        Question GetPreviousQuestion();
    }
}