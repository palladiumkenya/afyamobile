using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientRelationshipsViewModel:MvxViewModel, IClientRelationshipsViewModel
    {
        private string _search;
        private Client _selectedClient;
        private IEnumerable<Client> _clients;
        private IMvxCommand _searchCommand;
        private  IMvxCommand<Client> _clientSelectedCommand;
        private  IMvxCommand _addRelationshipCommand;
        private  IMvxCommand _clearSearchCommand;

        public string Search
        {
            get { return _search; }
            set { _search = value;RaisePropertyChanged(() =>Search ); }
        }
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; RaisePropertyChanged(() => SelectedClient); }
        }
        public IEnumerable<Client> Clients
        {
            get { return _clients; }
            set { _clients = value; RaisePropertyChanged(() => Clients); }
        }


        public IMvxCommand SearchCommand
        {
            get
            {
                _searchCommand = _searchCommand ?? new MvxCommand(SearchClient);
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
                _addRelationshipCommand = _addRelationshipCommand ?? new MvxCommand(AddRelationship);
                return _addRelationshipCommand;
            }
        }

        private void SearchClient()
        {
            throw new System.NotImplementedException();
        }
        private void ClearSearch()
        {
            throw new System.NotImplementedException();
        }
        private void SelectClient(Client client)
        {
            throw new System.NotImplementedException();
        }
        private void AddRelationship()
        {
            throw new System.NotImplementedException();
        }
    }
}