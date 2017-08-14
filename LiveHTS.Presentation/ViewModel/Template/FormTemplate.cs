using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class FormTemplate : IFormTemplate
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public string EncounterDisplay { get; set; }
        public bool HasEncounters { get; set; }
        public Guid DefaultEncounterTypeId { get; set; }
        public List<EncounterTemplateWrap> Encounters { get; set; }=new List<EncounterTemplateWrap>();

        public FormTemplate(Form r, string encounterDisplay = "ENCOUNTRES")
        {
            Id = r.Id;
            Display = r.Display;
            EncounterDisplay = encounterDisplay;
            DefaultEncounterTypeId = r.DefaultEncounterTypeId;
            HasEncounters = r.ClientEncounters.Count > 0;
        }
    }
}