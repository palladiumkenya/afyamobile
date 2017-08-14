using System;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientEncounterViewModel : MvxViewModel, IClientEncounterViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IEncounterService _encounterService;

        private readonly ISettings _settings;
        private ClientDTO _clientInfo;
        private Form _form;
        private Encounter _encounter;

        public ClientDTO ClientDTO
        {
            get { return _clientInfo; }
            set { _clientInfo = value; RaisePropertyChanged(() => ClientDTO); }
        }

        public Form Form
        {
            get { return _form; }
            set { _form = value; RaisePropertyChanged(() => Form); }
        }

        public Encounter Encounter
        {
            get { return _encounter; }
            set { _encounter = value; RaisePropertyChanged(() => Encounter); }
        }

        public ClientEncounterViewModel(ISettings settings, IDialogService dialogService, IEncounterService encounterService)
        {
            _settings = settings;
            _dialogService = dialogService;
            _encounterService = encounterService;
        }

        public void Init(string clientId,string formId,string encounterTypeId)
        {
            Form = _encounterService.LoadForm(new Guid(formId));

        }

        public override void ViewAppeared()
        {
            var client = _settings.GetValue("client", "");

            if (!string.IsNullOrWhiteSpace(client))
            {
                ClientDTO = JsonConvert.DeserializeObject<ClientDTO>(client);
            }
        }
    }
}