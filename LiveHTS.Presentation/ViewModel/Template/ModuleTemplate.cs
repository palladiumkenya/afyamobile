using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class ModuleTemplate : IModuleTemplate
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public List<FormTemplateWrap> AllForms { get; set; } = new List<FormTemplateWrap>();
        public decimal Rank { get; set; }

        public ModuleTemplate(Module r)
        {
            Id = r.Id;
            Display = r.Display;
            Rank = r.Rank;
        }
    }
}