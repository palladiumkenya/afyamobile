using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientRelationshipsViewModel : MvxViewModel, IClientRelationshipsViewModel
    {
        private readonly IRegistryService _registryService;
        private readonly ILookupService _lookupService;
        private readonly IDialogService _dialogService;
        private string _search;
        private Client _selectedClient;
        private IEnumerable<Client> _clients;
        private IMvxCommand _searchCommand;
        private IMvxCommand<Client> _clientSelectedCommand;
        private IMvxCommand _addRelationshipCommand;
        private IMvxCommand _clearSearchCommand;
        private IEnumerable<RelationshipType> _relationshipTypes;
        private RelationshipType _selectedRelationshipType;
        private string _clientId;
        private bool _showId;
        private string _partnerName;
        private IMvxCommand _addPersonCommand;
        private string _addPersonLabel;
        private readonly ISettings _settings;

        public ClientRelationshipsViewModel(IRegistryService registryService, IDialogService dialogService,
            ILookupService lookupService, ISettings settings)
        {
            _settings = settings;
            _registryService = registryService;
            _dialogService = dialogService;
            _lookupService = lookupService;

            RelationshipTypes = _lookupService.GetRelationshipTypes().ToList();
            if (RelationshipTypes.ToList().Count > 0)
                SelectedRelationshipType = RelationshipTypes.FirstOrDefault();
            //TODO: Remove ShowId
            ShowId = true;
        }

        public IEnumerable<RelationshipType> RelationshipTypes
        {
            get { return _relationshipTypes; }
            set
            {
                _relationshipTypes = value;
                RaisePropertyChanged(() => RelationshipTypes);
            }
        }

        public RelationshipType SelectedRelationshipType
        {
            get { return _selectedRelationshipType; }
            set
            {
                _selectedRelationshipType = value;
                RaisePropertyChanged(() => SelectedRelationshipType);
            }
        }

        public string RelType { get; private set; }

        public string ClientId
        {
            get { return _clientId; }
            set
            {
                _clientId = value;
                RaisePropertyChanged(() => ClientId);
                AddRelationshipCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ShowId
        {
            get { return _showId; }
            set
            {
                _showId = value;
                RaisePropertyChanged(() => ShowId);
            }
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
            set
            {
                _selectedClient = value;
                RaisePropertyChanged(() => SelectedClient);

                PartnerName = string.Empty;
                if (null != _selectedClient)
                {
                    PartnerName = SelectedClient.Person.FullName;
                }
                AddRelationshipCommand.RaiseCanExecuteChanged();
            }
        }

        public string PartnerName
        {
            get { return _partnerName; }
            set
            {
                _partnerName = value;
                RaisePropertyChanged(() => PartnerName);
            }
        }

        public string AddPersonLabel
        {
            get { return _addPersonLabel; }
            set
            {
                _addPersonLabel = value;
                RaisePropertyChanged(() => AddPersonLabel);
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
                _searchCommand = _searchCommand ?? new MvxCommand(SearchClient, CanSearch);
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

        public IMvxCommand AddRelationshipCommand
        {
            get
            {
                _addRelationshipCommand =
                    _addRelationshipCommand ?? new MvxCommand(AddRelationship, CanAddRelationship);
                return _addRelationshipCommand;
            }
        }

        public IMvxCommand AddPersonCommand
        {
            get
            {
                _addPersonCommand = _addPersonCommand ?? new MvxCommand(AddPerson, CanAddPerson);
                return _addPersonCommand;
            }
        }



        private void SearchClient()
        {
            Clients = _registryService.GetAllClients(Search);
        }

        public void Init(string id, string reltype)
        {
            RelType = reltype;
            ClientId = id;
            AddPersonLabel = $"Register New {RelType}";
        }

        public override void ViewAppeared()
        {
            RelationshipTypes = RelationshipTypes.Where(x => x.Description.ToLower() == RelType.ToLower()).ToList();
            AddPersonLabel = $"Register New {RelType}";
        }

        private bool CanSearch()
        {
            return !string.IsNullOrWhiteSpace(Search) && Search.Length > 2;
        }

        private void ClearSearch()
        {
            Search = string.Empty;
            Clients = new List<Client>();
        }

        private void SelectClient(Client client)
        {
            if (null == client)
            {
                SelectedClient = null;
                return;
            }
            SelectedClient = client;
        }

        private void AddRelationship()
        {
            _registryService.UpdateRelationShips(SelectedRelationshipType.Id, new Guid(ClientId), SelectedClient.Id);
            Close(this);
            ShowViewModel<DashboardViewModel>(new {id = ClientId});

        }

        private bool CanAddRelationship()
        {
            return null != SelectedClient && !string.IsNullOrEmpty(ClientId);
        }

        private void AddPerson()
        {
            ClearCache(_settings);
            ShowViewModel<ClientRegistrationViewModel>(new {reltype = RelType, indexId = ClientId});
        }

        private bool CanAddPerson()
        {
            return true;
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
    }
}