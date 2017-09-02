using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class EncounterViewModel :MvxViewModel, IEncounterViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IDashboardService _dashboardService;
        private readonly IInterviewService _interviewService;
        private readonly ILookupService _lookupService;
        protected readonly ISettings _settings;

        private Module _module;
        private Client _client;
        private List<FormTemplateWrap> _forms;
        private EncounterType _defaultEncounterType;

        public string Title { get; set; }

        public Client Client
        {
            get {  return _client; }
            set { _client = value; RaisePropertyChanged(() => Client ); }
        }

        public Module Module
        {
            get { return _module; }
            set
            {
                _module = value; RaisePropertyChanged(() => Module);
                var forms = Module.Forms.ToList();
                foreach (var form in forms)
                {
                    form.ClientEncounters = _interviewService.LoadEncounters(Client.Id, form.Id).ToList();
                }
                Forms = ConvertToFormWrapperClass(forms, this);
            }
        }
        public List<FormTemplateWrap> Forms
        {
            get { return _forms; }
            set { _forms = value; RaisePropertyChanged(() => Forms); }
        }
   
        public EncounterViewModel()
        {
            Title = "ENCOUNTERS";
            _dialogService = Mvx.Resolve<IDialogService>();
            _dashboardService = Mvx.Resolve<IDashboardService>();
            _interviewService = Mvx.Resolve<IInterviewService>();
            _lookupService = Mvx.Resolve<ILookupService>();
            _settings = Mvx.Resolve<ISettings>();
        }

        public void StartEncounter(FormTemplate formTemplate)
        {
            var clientEncounterDTO = ClientEncounterDTO.Create(Client.Id, formTemplate);
            var clientEncounterDTOJson = JsonConvert.SerializeObject(clientEncounterDTO);
            _settings.AddOrUpdateValue("client.encounter.dto", clientEncounterDTOJson);

            if (formTemplate.Display.ToLower().Contains("Test Form".ToLower()))
            {
                ShowViewModel<TestingViewModel>(new
                {
                    formId = formTemplate.Id.ToString(),
                    encounterTypeId = formTemplate.EncounterTypeId.ToString(),
                    mode = "new",
                    clientId =Client.Id.ToString(),
                    encounterId = ""
                });
                return;
            }

            //Linkage
            if (formTemplate.Display.ToLower().Contains("Linkage".ToLower()))
            {
                ShowViewModel<LinkageViewModel>(new
                {
                    formId = formTemplate.Id.ToString(),
                    encounterTypeId = formTemplate.EncounterTypeId.ToString(),
                    mode = "new",
                    clientId = Client.Id.ToString(),
                    encounterId = ""
                });
                return;
            }

            ShowViewModel<ClientEncounterViewModel>(new
            {
                formId = formTemplate.Id.ToString(),
                encounterTypeId = formTemplate.EncounterTypeId.ToString(),
                mode = "new",
                encounterId = ""
            });
        }

        public void ResumeEncounter(EncounterTemplate encounterTemplate)
        {
            var clientEncounterDTO = ClientEncounterDTO.Create(Client.Id, encounterTemplate);
            var clientEncounterDTOJson = JsonConvert.SerializeObject(clientEncounterDTO);
            _settings.AddOrUpdateValue("client.encounter.dto", clientEncounterDTOJson);

            if (encounterTemplate.FormDisplay.ToLower().Contains("Test Form".ToLower()))
            {
                ShowViewModel<TestingViewModel>(new
                {
                    formId = encounterTemplate.FormId.ToString(),
                    encounterTypeId = encounterTemplate.EncounterTypeId.ToString(),
                    mode = "open",
                    clientId = Client.Id.ToString(),
                    encounterId = encounterTemplate.Id.ToString()
                });
                return;
            }
            //Linkage
            if (encounterTemplate.FormDisplay.ToLower().Contains("Linkage".ToLower()))
            {
                ShowViewModel<LinkageViewModel>(new
                {
                    formId = encounterTemplate.FormId.ToString(),
                    encounterTypeId = encounterTemplate.EncounterTypeId.ToString(),
                    mode = "open",
                    clientId = Client.Id.ToString(),
                    encounterId = encounterTemplate.Id.ToString()
                });
                return;
            }
            ShowViewModel<ClientEncounterViewModel>(new
            {
                formId = encounterTemplate.FormId.ToString(),
                encounterTypeId = encounterTemplate.EncounterTypeId.ToString(),
                mode = "open",
                encounterId = encounterTemplate.Id.ToString()
            });
        }

        public void ReviewEncounter(EncounterTemplate encounterTemplate)
        {
            ResumeEncounter(encounterTemplate);
        }

        public async void DiscardEncounter(EncounterTemplate encounterTemplate)
        {
            try
            {
                var result = await _dialogService.ConfirmAction("Are you sure ?", "Delete this Encounter");
                if (result)
                {
                    _dashboardService.RemoveEncounter(encounterTemplate.Id);
                    Module = _dashboardService.LoadModule();
                }
            }
            catch (Exception e)
            {
                MvxTrace.Error(e.Message);
                _dialogService.Alert(e.Message, "Delete this Encounter");
            }
        }
        private static List<FormTemplateWrap> ConvertToFormWrapperClass(List<Form> forms, IEncounterViewModel encounterViewModel)
        {
            List<FormTemplateWrap> list = new List<FormTemplateWrap>();
            foreach (var r in forms)
            {
                foreach (var program in r.Programs)
                {
                    var f = new FormTemplate(r,program);
                    var pe = r.ClientEncounters.Where(x => x.EncounterTypeId == program.EncounterTypeId).ToList();
                    f.Encounters = ConvertToEncounterWrapperClass(pe, encounterViewModel, f.Display);
                    var fw = new FormTemplateWrap(encounterViewModel, f);
                    list.Add(fw);
                }
            }
            list = list.OrderBy(x => x.FormTemplate.Rank).ToList();
            return list;
        }

        private static List<EncounterTemplateWrap> ConvertToEncounterWrapperClass(List<Encounter> encounters, IEncounterViewModel encounterViewModel, string fDisplay)
        {
            List<EncounterTemplateWrap> list = new List<EncounterTemplateWrap>();
            foreach (var r in encounters)
            {
                list.Add(new EncounterTemplateWrap(encounterViewModel, new EncounterTemplate(r, fDisplay)));
            }
            return list;
        }
    }
}