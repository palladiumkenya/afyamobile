using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class DashboardViewModel:MvxViewModel,IDashboardViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IDashboardService _dashboardService;
        protected readonly ISettings _settings;
        private readonly ILookupService _lookupService;

        private Client _client;
        private List<RelationshipTemplateWrap> _relationships = new List<RelationshipTemplateWrap>();
        private Module _module;
        private MvxCommand _manageRegistrationCommand;

        public IEncounterViewModel EncounterViewModel { get; }
        public IPartnerViewModel PartnerViewModel { get; }
        public ISummaryViewModel SummaryViewModel { get; }
        public IMvxCommand ManageRegistrationCommand
        {
            get
            {
                _manageRegistrationCommand = _manageRegistrationCommand ?? new MvxCommand(ManageRegistration);
                return _manageRegistrationCommand;
            }
        }

        private void ManageRegistration()
        {
            ShowViewModel<ClientRegistrationViewModel>(new { id = Client.Id });
        }
        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value; RaisePropertyChanged(() => Client);
                PartnerViewModel.Client = EncounterViewModel.Client = Client;
            }
        }

        public Module Module
        {
            get { return _module; }
            set
            {
                _module = value; RaisePropertyChanged(() => Module);
                EncounterViewModel.Module = Module;
            }
        }

        public DashboardViewModel(ISettings settings, IDialogService dialogService, IDashboardService dashboardService, ILookupService lookupService)
        {
            _settings = settings;
            _dialogService = dialogService;
            _dashboardService = dashboardService;
            _lookupService = lookupService;

            EncounterViewModel = new EncounterViewModel();
            PartnerViewModel =new PartnerViewModel();
            SummaryViewModel = new SummaryViewModel();
       }
        public void Init(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;

            Client = _dashboardService.LoadClient(new Guid(id));
            Module = _dashboardService.LoadModule();            

            if (null != Client)
            {
                var clientJson = JsonConvert.SerializeObject(Client);
                _settings.AddOrUpdateValue("client", clientJson);

                var clientDto = ClientDTO.Create(Client);
                var clientDtoJson = JsonConvert.SerializeObject(clientDto);
                _settings.AddOrUpdateValue("client.dto", clientDtoJson);
            }
            if (null != Module)
            {
                var moduleJson = JsonConvert.SerializeObject(Module);
                _settings.AddOrUpdateValue("module", moduleJson);
            }
        }

        public override void ViewAppeared()
        {
            //Reload

            var clientJson = _settings.GetValue("client", "");
            var moduleJson = _settings.GetValue("module", "");


            if (null == Client)
            {
                if (!string.IsNullOrWhiteSpace(clientJson))
                {
                    Client = JsonConvert.DeserializeObject<Client>(clientJson);
                    var clientDto = ClientDTO.Create(Client);
                    var clientDtoJson = JsonConvert.SerializeObject(clientDto);
                    _settings.AddOrUpdateValue("client.dto", clientDtoJson);
                }
            }
            if (null == Module)
            {

                if (!string.IsNullOrWhiteSpace(moduleJson))
                {
                    Module = JsonConvert.DeserializeObject<Module>(moduleJson);
                }
            }
        }
    }
}