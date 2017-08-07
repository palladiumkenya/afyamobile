using System;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class EncounterTemplate: IEncounterTemplate
    {
        public Guid Id { get; set; }
        public DateTime EncounterDate { get; set; }
        public string Status { get; set; }
        public string Provider { get; set; }
        public string ElapsedTime { get; set; }
    }
}