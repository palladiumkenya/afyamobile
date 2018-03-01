using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IUserSummaryViewModel: IMvxViewModel
    {
        IEnumerable<UserSummary> Summaries { get; set; }
        IMvxCommand RefreshCommand { get; }
    }
}