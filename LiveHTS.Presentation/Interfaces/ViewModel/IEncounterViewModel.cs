using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IEncounterViewModel:IMvxViewModel
    {
        string Title { get; set; }
        Module Module { get; set; }
        Client Client { get; set; }
        List<FormTemplateWrap> Forms { get; set; }
        void StartEncounter(FormTemplate encounterTemplate);
        void ResumeEncounter(EncounterTemplate encounterTemplate);
        void ReviewEncounter(EncounterTemplate encounterTemplate);
        void DiscardEncounter(EncounterTemplate encounterTemplate);
    }
}