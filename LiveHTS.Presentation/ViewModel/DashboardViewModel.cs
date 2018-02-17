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

        private List<Module> FilterList(List<Module> list)
        {
            var final=new List<Module>();
            final.AddRange(list.Where(x=>x.Rank==1));

            if(Client.IsFamilyMember)
                final.AddRange(list.Where(x => x.Rank == 2));

            if (Client.IsPartner)
                final.AddRange(list.Where(x => x.Rank == 3));

            return final;
        }

        public DashboardViewModel(ISettings settings, IDialogService dialogService, IDashboardService dashboardService, ILookupService lookupService)
        {
            _settings = settings;
            _dialogService = dialogService;
            _dashboardService = dashboardService;
            _lookupService = lookupService;

            EncounterViewModel = new EncounterViewModel();
            FamilyMemberViewModel=new FamilyMemberViewModel();
            PartnerViewModel =new PartnerViewModel();
            SummaryViewModel = new SummaryViewModel();
       }
        public void Init(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;

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
            //var moduleJson = _settings.GetValue("module", "");
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
          
//            if (null == Module)
//            {
//
//                if (!string.IsNullOrWhiteSpace(moduleJson))
//                {
//                    Module = JsonConvert.DeserializeObject<Module>(moduleJson);
//                }
//            }
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
//            if (null != Module)
//            {
//                EncounterViewModel.Module = Module;
//            }
            if (null != Modules)
            {
                EncounterViewModel.Modules = Modules;
            }
        }

        public void GoBack()
        {
            var profile = _settings.GetValue("livehts.username", "");
            if (!string.IsNullOrWhiteSpace(profile))
            {
                ShowViewModel<AppDashboardViewModel>(new { username = profile });
            }
          
        }
    }
}