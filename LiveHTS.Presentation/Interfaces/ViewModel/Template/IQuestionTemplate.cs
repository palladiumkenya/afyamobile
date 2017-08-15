using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IQuestionTemplate
    {
        Guid Id { get; set; }
        string Display { get; set; }
        Concept Concept { get; set; }
        bool ShowSingleObs { get; set; }
        bool ShowTextObs { get; set; }
    }
}