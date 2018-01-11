using System.Collections.Generic;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class RemoteRegistryViewModel:MvxViewModel,IRemoteRegistryViewModel
    {
        public ICohortViewModel CohortViewModel { get; set; }
        public IRemoteSearchViewModel RemoteSearchViewModel { get; set; }

        public RemoteRegistryViewModel()
        {
            CohortViewModel=new CohortViewModel();
            CohortViewModel.Parent = this;
            RemoteSearchViewModel=new RemoteSearchViewModel();
            RemoteSearchViewModel.Parent = this;
        }
    }
}