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
        private IMvxCommand _enrollCommand;

        private List<Module> _modules=new List<Module>();
        private bool _showEnroll;


        public int GetActiveTab()
        {
            var callerId = _settings.GetValue("activetabId", 0);
            return callerId;
        }

        public IEncounterViewModel EncounterViewModel { get; }
        public IFamilyMemberViewModel FamilyMemberViewModel { get; }
        public IPartnerViewModel PartnerViewModel { get; }
        public ISummaryViewModel SummaryViewModel { get; }

        public bool ShowEnroll
        {
            get { return _showEnroll; }
            set { _showEnroll = value; RaisePropertyChanged(() => ShowEnroll); }
        }


        public IMvxCommand ManageRegistrationCommand
        {
            get
            {
                _manageRegistrationCommand = _manageRegistrationCommand ?? new MvxCommand(ManageRegistration);
                return _manageRegistrationCommand;
            }
        }

        public IMvxCommand EnrollCommand
        {
            get
            {
                _enrollCommand = _enrollCommand ?? new MvxCommand(Enroll);
                return _enrollCommand;
            }
        }

        private void Enroll()
        {
            ShowViewModel<ClientRegistrationViewModel>(new { id = Client.Id, enroll="true" });
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
                PartnerViewModel.Client = EncounterViewModel.Client =FamilyMemberViewModel.Client= Client;
                ShowEnroll = null!=Client.PreventEnroll&&Client.PreventEnroll.Value;

                if (Client.Relationships.Any(x => x.IsFamilyRelation()))
                {

                    _settings.AddOrUpdateValue("emod", "fam");
                };
                if (Client.Relationships.Any(x => x.IsPatner()))
                {

                    _settings.AddOrUpdateValue("emod", "pns");
                };
            }
        }

        public Module Module
        {
            get { return _module; }
            set
            {
                _module = value; RaisePropertyChanged(() => Module);
               // EncounterViewModel.Module = Module;
            }
        }
        public List<Module> Modules
        {
            get { return _modules; }
            set
            {
                var list = value;
                _modules = FilterList(list);
                RaisePropertyChanged(() => Modules);
                EncounterViewModel.Modules = Modules;
            }
        }

        //TODO: Start form here
        private List<Module> FilterList(List<Module> list)
        {
            var final = new List<Module>();

            if (!Client.DisableHts())
                final.Add(list.FirstOrDefault(x => x.Rank == 1));

            var emode = _settings.GetValue("emod", "");
            if (!string.IsNullOrWhiteSpace(emode))
            {
                if (emode == "fam")
                {
                    final.Add(list.FirstOrDefault(x => x.Rank == 3));
                }

                if (emode == "pns")
                {
                    final.Add(list.FirstOrDefault(x => x.Rank == 2));
                }
            }

            return final;
        }

        public DashboardViewModel(ISettings settings, IDialogService dialogService, IDashboardService dashboardService,
            ILookupService lookupService)
        {
            _settings = settings;
            _dialogService = dialogService;
            _dashboardService = dashboardService;
            _lookupService = lookupService;

            EncounterViewModel = new EncounterViewModel();
            EncounterViewModel.Parent = this;
            FamilyMemberViewModel = new FamilyMemberViewModel();
            FamilyMemberViewModel.Parent = this;
            PartnerViewModel = new PartnerViewModel();
            PartnerViewModel.Parent = this;
            SummaryViewModel = new SummaryViewModel();
        }

        public void Init(string id,string callerId,string mode)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;

            if (!string.IsNullOrWhiteSpace(callerId))
            {
                _settings.AddOrUpdateValue("callerId", callerId);
                _settings.AddOrUpdateValue("activetabId",1);
            }
            else
            {
                if (_settings.Contains("callerId"))
                    _settings.DeleteValue("callerId");
                if (_settings.Contains("activetabId"))
                    _settings.DeleteValue("activetabId");
            }
            if (!string.IsNullOrWhiteSpace(mode))
            {
                _settings.AddOrUpdateValue("emod", mode);
            }
            else
            {
                if (_settings.Contains("emod"))
                    _settings.DeleteValue("emod");
            }

            Client = _dashboardService.LoadClient(new Guid(id));
            Modules = _dashboardService.LoadModules();

            if (null != Client)
            {
                var clientJson = JsonConvert.SerializeObject(Client);
                _settings.AddOrUpdateValue("client", clientJson);

                var clientDto = ClientDTO.Create(Client);
                var clientDtoJson = JsonConvert.SerializeObject(clientDto);
                _settings.AddOrUpdateValue("client.dto", clientDtoJson);
            }
//            if (null != Module)
//            {
//                var moduleJson = JsonConvert.SerializeObject(Module);
//                _settings.AddOrUpdateValue("module", moduleJson);
//            }
            if (null != Modules)
            {
                var modulesJson = JsonConvert.SerializeObject(Modules);
                _settings.AddOrUpdateValue("modules", modulesJson);
            }
        }

        public override void ViewAppeared()
        {
            //Reload
            var clientJson = _settings.GetValue("client", "");
            var modulesJson = _settings.GetValue("modules", "");

            if (null == Client)
            {
                if (!string.IsNullOrWhiteSpace(clientJson))
                {
                    var client= JsonConvert.DeserializeObject<Client>(clientJson);
                    Client = JsonConvert.DeserializeObject<Client>(clientJson);
                    var clientDto = ClientDTO.Create(Client);
                    var clientDtoJson = JsonConvert.SerializeObject(clientDto);
                    _settings.AddOrUpdateValue("client.dto", clientDtoJson);
                }
            }
            if (null == Modules)
            {

                if (!string.IsNullOrWhiteSpace(modulesJson))
                {
                    Modules = JsonConvert.DeserializeObject<List<Module>>(modulesJson);
                }
            }
            if (null != Client)
            {
                PartnerViewModel.Client = EncounterViewModel.Client = Client;
            } 
        }

        public void GoBack()
        {
            var callerId = _settings.GetValue("callerId", "");
            if (!string.IsNullOrWhiteSpace(callerId))
            {
                Close(this);
                ShowViewModel<DashboardViewModel>(new {id = callerId});
                return;
            }


            var profile = _settings.GetValue("livehts.username", "");
            if (!string.IsNullOrWhiteSpace(profile))
            {
                ShowViewModel<AppDashboardViewModel>(new { username = profile });
            }
          
        }

        public void ShowDashboard(string id, string callerId, string mode)
        {
            Close(this);
            ShowViewModel<DashboardViewModel>(new {id = id, callerId = callerId,mode=mode});
        }
    }
}