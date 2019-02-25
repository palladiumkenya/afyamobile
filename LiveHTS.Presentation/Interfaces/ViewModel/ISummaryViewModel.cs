using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ISummaryViewModel : IMvxViewModel
    {
        string Title { get; set; }
        Client Client { get; set; }
        List<ClientSummary> Summaries { get; set; }
    }
}