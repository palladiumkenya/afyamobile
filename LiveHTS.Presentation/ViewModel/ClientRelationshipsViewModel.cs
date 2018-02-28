using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

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
        private string _indexClientId;
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

        public string IndexClientId
        {
            get { return _indexClientId; }
            set
            {
                _indexClientId = value;
                RaisePropertyChanged(() => IndexClientId);
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
            IndexClientId = id;
            AddPersonLabel = $"Register New {RelType}";
            RelationshipTypes = _lookupService.GetRelationshipTypes().ToList().Where(x => x.Description.ToLower() == RelType.ToLower()).ToList(); 

            if (!string.IsNullOrEmpty(RelType))
            {
                _settings.AddOrUpdateValue("RelType", RelType);
            }
            if (!string.IsNullOrEmpty(RelType))
            {
                _settings.AddOrUpdateValue("rIndexClientId", IndexClientId);
            }
            if (!string.IsNullOrEmpty(RelType))
            {
                _settings.AddOrUpdateValue("AddPersonLabel", AddPersonLabel);
            }
            if (RelationshipTypes.ToList().Count>0)
            {
                _settings.AddOrUpdateValue("RelationshipTypes", JsonConvert.SerializeObject(RelationshipTypes.ToList()));
            }
        }

        public override void ViewAppeared()
        {
            var relType = _settings.GetValue("RelType", "");
            var indexClientId = _settings.GetValue("rIndexClientId", "");
            var addPersonLabel = _settings.GetValue("AddPersonLabel", "");
            var relationshipTypesJson = _settings.GetValue("RelationshipTypes", "");

            if (!string.IsNullOrEmpty(RelType)&& !string.IsNullOrEmpty(relType))
            {
                RelType = relType;
            }
            if (!string.IsNullOrEmpty(IndexClientId) && !string.IsNullOrEmpty(indexClientId))
            {
                IndexClientId = indexClientId;
            }
            if (!string.IsNullOrEmpty(AddPersonLabel) && !string.IsNullOrEmpty(addPersonLabel))
            {
                AddPersonLabel = addPersonLabel;
            }
            if (!RelationshipTypes.Any()&& !string.IsNullOrEmpty(relationshipTypesJson))
            {
                RelationshipTypes = JsonConvert.DeserializeObject<List<RelationshipType>>(relationshipTypesJson);
            }
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
            _registryService.UpdateRelationShips(SelectedRelationshipType.Id, new Guid(IndexClientId), SelectedClient.Id);
            //Close(this);
            ShowViewModel<DashboardViewModel>(new {id = IndexClientId });
        }

        private bool CanAddRelationship()
        {
            return null != SelectedClient && !string.IsNullOrEmpty(IndexClientId);
        }

        private void AddPerson()
        {
            ClearCache(_settings);
            ShowViewModel<ClientRegistrationViewModel>(new {reltype = RelType, indexId = IndexClientId});
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