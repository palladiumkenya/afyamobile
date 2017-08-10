using System;
using System.Xml.Linq;
using LiveHTS.Core.Model.Interview;
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

        public EncounterTemplate(Encounter encounter)
        {
            Id = encounter.Id;
            EncounterDate = encounter.EncounterDate;
            Status = encounter.Status;
            Provider = "J.Kimani";
            ElapsedTime = "x days";
        }
    }
}