using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Engine
{
    public interface INavigationEngine
    {
        List<SetResponse> GetActions(Manifest currentManifest, Guid currentQuestionId);
        Question GetLiveQuestion(Manifest currentManifest, Guid? currentQuestionId=null);
        Question GetNextQuestion(Guid currentQuestionId, Manifest currentManifest);
        Question GetPreviousQuestion(Guid currentQuestionId, Manifest currentManifest);
        Question GetQuestion(Guid questionId, Manifest currentManifest);
        Question EvaluateSelf(Question question, Manifest currentManifest, Guid? currentQuestionId = null);
    }
}