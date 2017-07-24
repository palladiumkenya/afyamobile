﻿using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class RegistryViewModel:MvxViewModel,IRegistryViewModel
    {
        private readonly IRegistryService _registryService;
        private IEnumerable<Client> _clients;
        private bool _isBusy;
        private string _search;
        private IMvxCommand _searchCommand;
        private IMvxCommand _clearSearchCommand;

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

        private void ClearSearch()
        {
            Search = string.Empty;
            LoadClients();
        }

        private void SearchClients()
        {
            IsBusy = true;
            Clients = _registryService.GetAllClients(Search);
            IsBusy = false;
        }
        private bool CanSearch()
        {
            return !string.IsNullOrWhiteSpace(Search) && Search.Length > 2;
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

        public RegistryViewModel(IRegistryService registryService)
        {
            _registryService = registryService;
        }

        public override void Start()
        {
          LoadClients();
        }

        private void LoadClients()
        {
            IsBusy = true;
            Clients = _registryService.GetAllClients();
            IsBusy = false;
        }
    }
}