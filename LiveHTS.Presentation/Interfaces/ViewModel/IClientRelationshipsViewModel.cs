using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientRelationshipsViewModel
    {
        string Search { get; set; }
        Client SelectedClient { get; set; }
        IEnumerable<Client> Clients { get; set; }
        IMvxCommand SearchCommand { get; }
        IMvxCommand ClearSearchCommand { get; }
        IMvxCommand<Client> ClientSelectedCommand { get; }
        IMvxCommand AddRelationshipCommand { get; }
    }
}