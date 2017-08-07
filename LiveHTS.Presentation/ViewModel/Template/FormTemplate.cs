using System;
using System.Collections.Generic;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class FormTemplate:IFormTemplate
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public string EncounterDisplay { get; set; }
        //public List<IEncounterTemplate> EncounterTemplates { get; set; }
    }
}