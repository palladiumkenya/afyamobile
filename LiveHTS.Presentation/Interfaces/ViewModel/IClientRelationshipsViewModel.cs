using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientRelationshipsViewModel
    {
        string ClientId { get; set; }
        bool ShowId { get; set; }
        string RelType { get;  }
        string Search { get; set; }
        Client SelectedClient { get; set; }
        string PartnerName { get; set; }
        string AddPersonLabel { get; set; }
        IEnumerable<Client> Clients { get; set; }
        IEnumerable<RelationshipType> RelationshipTypes { get; set; }
        RelationshipType SelectedRelationshipType { get; set; }
        IMvxCommand SearchCommand { get; }
        IMvxCommand ClearSearchCommand { get; }
        IMvxCommand<Client> ClientSelectedCommand { get; }
        IMvxCommand AddRelationshipCommand { get; }
        IMvxCommand AddPersonCommand { get; }

    }
}