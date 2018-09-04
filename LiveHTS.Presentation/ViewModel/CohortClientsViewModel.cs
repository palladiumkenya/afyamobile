using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class CohortClientsViewModel:MvxViewModel,ICohortClientsViewModel
    {
        private readonly ISettings _settings;
        private readonly IRegistryService _registryService;
        private readonly IDeviceSetupService _deviceSetupService;
        private readonly IDialogService _dialogService;
        private readonly IChohortClientsSyncService _chohortClientsSyncService;
        private readonly IClientSyncService _clientSyncService;
        private readonly IEncounterService _encounterService;
        private IEnumerable<Client> _clients;
        private bool _isBusy;
        private string _search;
        private IMvxCommand _searchCommand;
        private IMvxCommand _clearSearchCommand;
        private Client _selectedClient;
        private IMvxCommand<Client> _clientSelectedCommand;


        public Device Device { get; set; }
        public ServerConfig Local { get; set; }
        public string Address { get; set; }
        public Guid CohortId { get; set; }
        public IRemoteRegistryViewModel Parent { get; set; }

        public Guid AppPracticeId
        {
            get { return GetGuid("livehts.practiceid"); }
        }

        public string Search
        {
            get { return _search; }
            set
            {
                _search = value; 
                RaisePropertyChanged(() => Search);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; RaisePropertyChanged(() => SelectedClient);}
        }

        public IEnumerable<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                RaisePropertyChanged(() => Clients);
            }
        }

        public IMvxCommand SearchCommand
        {
            get
            {
                _searchCommand = _searchCommand ?? new MvxCommand(SearchClients, CanSearch);
                return _searchCommand;
            }
        }

        public IMvxCommand ClearSearchCommand
        {
            get
            {
                _clearSearchCommand = _clearSearchCommand ?? new MvxCommand(ClearSearch);
                return _clearSearchCommand;
            }
        }

        public IMvxCommand<Client> ClientSelectedCommand
        {
            get
            {
                _clientSelectedCommand = _clientSelectedCommand ?? new MvxCommand<Client>(SelectClient);
                return _clientSelectedCommand;
            }
        }

     

      

        private void DeletePerson()
        {
            throw new System.NotImplementedException();
        }

       
        private async void SelectClient(Client selectedClient)
        {
            if (null == selectedClient)
                return;
            IsBusy = true;
            _dialogService.ShowWait($"Downloading,Please wait...");
            var remoteData = await _clientSyncService.DownloadClient(Address, selectedClient.Id);
            if (null != remoteData)
            {
                SelectedClient = remoteData.Client;
                var encounters = remoteData.Encounters;
                await _registryService.Download(SelectedClient, encounters);
            }
            _dialogService.HideWait();
            IsBusy = false;
            ShowViewModel<DashboardViewModel>(new { id = SelectedClient.Id });
        }


        private void ClearSearch()
        {
            Search = string.Empty;
            LoadClients();
        }

        private void SearchClients()
        {
            IsBusy = true;
            Clients = _registryService.GetAllCohortClients(CohortId,Search);
            IsBusy = false;
        }
        private bool CanSearch()
        {
            return !string.IsNullOrWhiteSpace(Search) && Search.Length > 0;
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value; 
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public CohortClientsViewModel( ISettings settings, IRegistryService registryService, IDialogService dialogService, IChohortClientsSyncService chohortClientsSyncService, IDeviceSetupService deviceSetupService, IClientSyncService clientSyncService, IEncounterService encounterService)
        {
            _settings = settings;
            _registryService = registryService;
            _dialogService = dialogService;
            _chohortClientsSyncService = chohortClientsSyncService;
            _deviceSetupService = deviceSetupService;
            _clientSyncService = clientSyncService;
            _encounterService = encounterService;
        }

        public void Init(string id)
        {
            LoadInit();
            if (!string.IsNullOrWhiteSpace(id))
            {
                _settings.AddOrUpdateValue("cohortId", id);
                CohortId = new Guid(id);
            }
            //LoadClients();   
        }

        public override void ViewAppeared()
        {
            LoadInit();
            var cohortJson = _settings.GetValue("cohortId", "");
            if (!string.IsNullOrWhiteSpace(cohortJson))
            {
                CohortId = new Guid(cohortJson);
            }
            LoadClients();
        }

        private void LoadInit()
        {
            var deviceJson = _settings.GetValue("device.id", "");
            var hapiLocal = _settings.GetValue("hapi.local", "");

            if (null == Device && !string.IsNullOrWhiteSpace(deviceJson))
            {
                Device = JsonConvert.DeserializeObject<Device>(deviceJson);
            }
            else
            {
                Device = _deviceSetupService.GetDefault();
                if (null == Device)
                {
                    Device = new Device();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(Device);
                    _settings.AddOrUpdateValue("device.id", json);
                }
            }

            if (null == Local && !string.IsNullOrWhiteSpace(hapiLocal))
            {
                Local = JsonConvert.DeserializeObject<ServerConfig>(hapiLocal);
            }
            else
            {
                Local = _deviceSetupService.GetLocal();
                if (null == Local)
                {
                    Local = new ServerConfig();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(Local);
                    _settings.AddOrUpdateValue("hapi.local", json);
                }
            }
            Address = Local.Address;
        }
        private async void LoadClients()
        {
            IsBusy = true;

            var  clients= _registryService.GetAllCohortClients(CohortId).ToList();
            if (clients.Count == 0)
            {
                _dialogService.ShowWait("Downloading,Please wait...");
                
                var remoteData = await _chohortClientsSyncService.GetClients(Address, CohortId.ToString(),AppPracticeId);
                if (remoteData.Count > 0)
                {
                    Clients = remoteData.Select(x => x.Client).ToList();
//                    foreach (var c in clients)
//                    {
//                        _registryService.Save(c,CohortId);
//                    }
//
//                    Clients = _registryService.GetAllCohortClients(CohortId).ToList();
                }
                else
                {
                    Clients=new List<Client>();
                    _dialogService.HideWait();
                    _dialogService.ShowToast("No data found!");
                    IsBusy = false; return;
                }
                _dialogService.HideWait();
            }
            else
            {
                Clients = clients;
            }
            IsBusy = false;
        }
        public Guid GetGuid(string key)
        {
            var guid = _settings.GetValue(key, "");

            if (string.IsNullOrWhiteSpace(guid))
                return Guid.Empty;

            return new Guid(guid);
        }
    }
}