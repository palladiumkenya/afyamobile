using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class FormTemplate:IFormTemplate
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public string EncounterDisplay { get; set; }
        //public List<IEncounterTemplate> EncounterTemplates { get; set; }

        public FormTemplate(Form r,string encounterDisplay = "ENCOUNTRES")
        {
            Id = r.Id;
            Display = r.Display;
            EncounterDisplay = encounterDisplay;
        }
    }
}