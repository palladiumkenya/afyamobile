using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class ModuleTemplate:IModuleTemplate
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public List<FormTemplate> FormTemplates { get; set; }=new List<FormTemplate>();
        public decimal Rank { get; set; }

        public ModuleTemplate(Module r)
        {
            Id = r.Id;
            Display = r.Display;
        }
        public ModuleTemplate(Module r, Module program)
        {
            Id = r.Id;
            Display = r.Display;
             Rank = r.Rank;
  
        }
    }
}