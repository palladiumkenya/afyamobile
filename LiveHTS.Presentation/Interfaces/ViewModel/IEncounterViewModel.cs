﻿using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IEncounterViewModel:IMvxViewModel
    {
        IDashboardViewModel Parent { get; set; }
        string Title { get; set; }
        List<ModuleTemplateWrap> AllModules { get; set; }
        Module Module { get; set; }
        List<Module> Modules { get; set; }
        IndexClientDTO IndexClient { get; set; }
        Client Client { get; set; }
        List<FormTemplateWrap> Forms { get; set; }
        List<FormTemplateWrap> FormsFamily { get; set; }
        List<FormTemplateWrap> FormsPartner { get; set; }
        void StartEncounter(FormTemplate encounterTemplate);
        void ResumeEncounter(EncounterTemplate encounterTemplate);
        void ReviewEncounter(EncounterTemplate encounterTemplate);
        void DiscardEncounter(EncounterTemplate encounterTemplate);
    }
}
