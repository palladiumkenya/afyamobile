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
        private ClientEncounterDTO _clientEncounterInfo;
        private Form _form;
        private Encounter _encounter;
        private ClientDTO _clientDTO;

        public Guid UserId
        {
            get
            {
                return _settings.GetValue("livehts.userid",Guid.Empty);
            }
        }

        public string UserName
        {
            get
            {
                return _settings.GetValue("livehts.username", "admin");
            }
        }

        public Guid ProviderId
        {
            get
            {
                return _settings.GetValue("livehts.providerid", Guid.Empty);
            }
        }

        public string ProviderName
        {
            get
            {
                return _settings.GetValue("livehts.providername", "");
            }
        }

        public ClientDTO ClientDTO
        {
            get { return _clientDTO; }
            set { _clientDTO = value; ; RaisePropertyChanged(() => ClientDTO); }
        }

        public ClientEncounterDTO ClientEncounterDTO
        {
            get { return _clientEncounterInfo; }
            set { _clientEncounterInfo = value; RaisePropertyChanged(() => ClientEncounterDTO); }
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

        public void Init(string formId,string mode)
        {
            if (null == Form)
            {
                Form = _encounterService.LoadForm(new Guid(formId));
                if (null != Form)
                {
                    var formJson = JsonConvert.SerializeObject(Form);
                    _settings.AddOrUpdateValue("client.form", formJson);
                }
            }

            var clientJson = _settings.GetValue("client", "");
            var clientEncounterJson = _settings.GetValue("client.encounter", "");

            if (!string.IsNullOrWhiteSpace(clientJson))
            {
                ClientDTO = JsonConvert.DeserializeObject<ClientDTO>(clientJson);
            }

            if (!string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                ClientEncounterDTO = JsonConvert.DeserializeObject<ClientEncounterDTO>(clientEncounterJson);
            }

            

            if (mode == "new")
            {
                //  New Encounter

                Encounter = _encounterService.StartEncounter(ClientEncounterDTO.FormId,
                    ClientEncounterDTO.EncounterTypeId, ClientEncounterDTO.ClientId, ProviderId, UserId);
            }
            else
            {
                //  Load Encounter

                Encounter = _encounterService.LoadEncounter(ClientEncounterDTO.FormId,
                    ClientEncounterDTO.EncounterTypeId, ClientEncounterDTO.ClientId,true);
            }
        }

        public override void ViewAppeared()
        {
            var clientJson = _settings.GetValue("client", "");
            var clientEncounterJson = _settings.GetValue("client.encounter", "");
            var formJson = _settings.GetValue("client.form", "");

            if (!string.IsNullOrWhiteSpace(clientJson))
            {
                ClientDTO = JsonConvert.DeserializeObject<ClientDTO>(clientJson);
            }

            if (!string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                ClientEncounterDTO = JsonConvert.DeserializeObject<ClientEncounterDTO>(clientEncounterJson);
            }

            if (!string.IsNullOrWhiteSpace(formJson))
            {
                Form = JsonConvert.DeserializeObject<Form>(formJson);
            }

        }
        
    }
}