using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IModuleTemplate
    {
        Guid Id { get; set; }
        string Display { get; set; }
        List<FormTemplateWrap> AllForms { get; set; }
        decimal Rank { get; set; }
    }
}