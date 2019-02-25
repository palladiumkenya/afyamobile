using System;
using System.Collections.Generic;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace LiveHTS.Presentation.ViewModel
{
    public class RegistryViewModel:MvxViewModel,IRegistryViewModel
    {
        private readonly ISettings _settings;
        private readonly IRegistryService _registryService;
        private readonly IDeviceSetupService _deviceSetupService;
        private IEnumerable<Client> _clients;
        private bool _isBusy;
        private string _search;
        private IMvxCommand _searchCommand;
        private IMvxCommand _clearSearchCommand;
        
        private Client _selectedClient;
        private IMvxCommand<Client> _clientSelectedCommand;
        private  IMvxCommand _registerClientCommand;
        private  IMvxCommand _openRemoteRegisteryCommand;
        private readonly IDialogService _dialogService;

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

        public IMvxCommand RegisterClientCommand
        {
            get
            {
                _registerClientCommand = _registerClientCommand ?? new MvxCommand(RegisterClient);
                return _registerClientCommand;
            }
        }

        public IMvxCommand OpenRemoteRegisteryCommand
        {
            get
            {
                _openRemoteRegisteryCommand = _openRemoteRegisteryCommand ?? new MvxCommand(OpenRemoteRegistery);
                return _openRemoteRegisteryCommand;
            }
        }

        private void OpenRemoteRegistery()
        {
         
            if (_deviceSetupService.HasPulledData())
            {
                ShowViewModel<RemoteRegistryViewModel>();
            }
            else
            {
                _dialogService.Alert("Please Pull Data before accessing the registry !");
            }

        }


        private void DeletePerson()
        {
            throw new System.NotImplementedException();
        }

        private void RegisterClient()
        {
            if (_deviceSetupService.HasPulledData())
            {
                ClearCache(_settings);
                ShowViewModel<ClientRegistrationViewModel>(new { mode = "new" });
            }
            else
            {
                _dialogService.Alert("Please Pull Data before proceeding !");
            }
        }

        private void SelectClient(Client selectedClient)
        {
            if(null==selectedClient)
                return;
            SelectedClient = selectedClient;
            ShowViewModel<DashboardViewModel>(new {id = SelectedClient.Id});
        }


        private void ClearSearch()
        {
            Search = string.Empty;
            LoadClients();
        }

        private void SearchClients()
        {
            IsBusy = true;
            Clients = _registryService.GetAllSiteClients(AppPracticeId,Search);
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
                //ManageStatus();
            }
        }

        public RegistryViewModel(IRegistryService registryService, ISettings settings, IDialogService dialogService, IDeviceSetupService deviceSetupService)
        {
            _registryService = registryService;
            _settings = settings;
            _dialogService = dialogService;
            _deviceSetupService = deviceSetupService;
        }

        public override void Start()
        {

            base.Start();
            LoadClients();
        }

        private void LoadClients()
        {
            IsBusy = true;
            Clients = _registryService.GetAllSiteClients(AppPracticeId);
            IsBusy = false;
        }

        private void ClearCache(ISettings settings)
        {

            if (settings.Contains(nameof(ClientDemographicViewModel)))
                settings.DeleteValue(nameof(ClientDemographicViewModel));

            if (settings.Contains(nameof(ClientContactViewModel)))
                settings.DeleteValue(nameof(ClientContactViewModel));

            if (settings.Contains(nameof(ClientProfileViewModel)))
                settings.DeleteValue(nameof(ClientProfileViewModel));

            if (settings.Contains(nameof(ClientEnrollmentViewModel)))
                settings.DeleteValue(nameof(ClientEnrollmentViewModel));
        }

        public Guid GetGuid(string key)
        {
            var guid = _settings.GetValue(key, "");

            if (string.IsNullOrWhiteSpace(guid))
                return Guid.Empty;

            return new Guid(guid);
        }

        private void ManageStatus()
        {
            if (IsBusy)
            {
                Common.StatusInfo.Show(_dialogService);
            }
            else
            {
                Common.StatusInfo.Close(_dialogService);
            }
        }
    }
}