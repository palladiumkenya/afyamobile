using System;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Engine
{
    public interface IDirector
    {
        Question GetLiveQuestion(Manifest currentManifest);
        Question GetNextQuestion(Guid currentQuestionId, Manifest currentManifest);
        Question GetPreviousQuestion(Guid currentQuestionId, Manifest currentManifest);
    }
}