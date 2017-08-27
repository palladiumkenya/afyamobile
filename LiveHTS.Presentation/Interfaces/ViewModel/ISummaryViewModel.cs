using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ISummaryViewModel: IMvxViewModel
    {
        string Title { get; set; }
    }
}