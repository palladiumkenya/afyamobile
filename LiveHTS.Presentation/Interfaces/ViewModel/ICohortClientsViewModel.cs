using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ICohortClientsViewModel
    {
        Device Device { get; set; }
        ServerConfig Local { get; set; }
        string Address { get; set; }
        Guid CohortId { get; set; }
        IRemoteRegistryViewModel Parent { get; set; }
        string Search { get; set; }
        Client SelectedClient { get; set; }
        IEnumerable<Client> Clients { get; set; }
        IMvxCommand SearchCommand { get; }
        IMvxCommand ClearSearchCommand { get; }
        IMvxCommand<Client> ClientSelectedCommand { get; }
        bool IsBusy { get; set; }
    }
}