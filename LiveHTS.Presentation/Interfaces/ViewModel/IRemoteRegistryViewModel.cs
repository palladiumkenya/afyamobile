using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IRemoteRegistryViewModel
    {
        ICohortViewModel CohortViewModel { get; set; }
        IRemoteSearchViewModel RemoteSearchViewModel { get; set; }
    }
}