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
        public Guid EncounterTypeId { get; set; }
        public string EncounterTypeDisplay { get; set; }
        public string EncounterTypeDescription { get; set; }
        public decimal Rank { get; set; }
        public bool HasEncounters { get; set; }
        public Guid DefaultEncounterTypeId { get; set; }
        public List<EncounterTemplateWrap> Encounters { get; set; }=new List<EncounterTemplateWrap>();

        public FormTemplate(Form r)
        {
            Id = r.Id;
            Display = r.Display;
            DefaultEncounterTypeId = r.DefaultEncounterTypeId;
            HasEncounters = r.ClientEncounters.Count > 0;
        }
        public FormTemplate(Form r,Program program)
        {
            Id = r.Id;
            Display = r.Display;
            EncounterTypeId = program.EncounterTypeId;
            EncounterTypeDisplay = program.Display;
            EncounterTypeDescription = program.Description;
            Rank = program.Rank;
            DefaultEncounterTypeId = r.DefaultEncounterTypeId;
            HasEncounters = r.ClientEncounters.Count > 0;
        }
    }
}