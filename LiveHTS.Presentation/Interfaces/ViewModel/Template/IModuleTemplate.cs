using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Presentation.ViewModel.Template;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IModuleTemplate
    {
        Guid Id { get; set; }
        string Display { get; set; }
        List<FormTemplate> FormTemplates { get; set; }
        decimal Rank { get; set; }
    }
}