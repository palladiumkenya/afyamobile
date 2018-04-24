using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class FormTemplate : IFormTemplate
    {
        public bool ShowFormName { get; set; } = false;
        public Guid Id { get; set; }
        public string Display { get; set; }
        public string EncounterDisplay { get; set; }
        public Guid EncounterTypeId { get; set; }
        public string EncounterTypeDisplay { get; set; }
        public string EncounterTypeDescription { get; set; }
        public decimal Rank { get; set; }
        public bool ConsentRequired { get; set; }
        public bool HasConsent { get; set; }
        public bool HasEncounters { get; set; }
        public Guid DefaultEncounterTypeId { get; set; }
        public List<EncounterTemplateWrap> Encounters { get; set; }=new List<EncounterTemplateWrap>();
        public bool Block { get; set; }

        public FormTemplate(Form r)
        {
            Id = r.Id;
            Display = r.Display;
            DefaultEncounterTypeId = r.DefaultEncounterTypeId;
            HasEncounters = r.ClientEncounters.Count > 0;
            ConsentRequired = r.ConsentRequired;
            HasConsent = r.HasConsent;
            Block = r.Block;
        }
        public FormTemplate(Form r,Program program)
        {
            Id = r.Id;
            Display = r.Display;
            EncounterTypeId = program.EncounterTypeId;
            EncounterTypeDisplay = SetDisplay(r, program);
            EncounterTypeDescription = program.Description;
            Rank = program.Rank;
            DefaultEncounterTypeId = r.DefaultEncounterTypeId;
            HasEncounters = r.ClientEncounters.Count > 0;
            ConsentRequired = r.ConsentRequired;
            HasConsent = r.HasConsent;
            Block = r.Block;
        }

        private string SetDisplay(Form form, Program program)
        {
            if (form.IsRepeat)
            {
                if (program.Display.IsSameAs("Pre Test"))
                {
                    return "Pre Test - (REPEAT)";
                }

                if (program.Display.IsSameAs("Testing"))
                {
                    return "Testing  - (REPEAT)";
                }
            }
            

            return program.Display;
        }
    }
}