using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IQuestionTemplate
    {
        Guid Id { get; set; }
        string Display { get; set; }
    }
}