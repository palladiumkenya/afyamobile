using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class SummaryViewModel : MvxViewModel,ISummaryViewModel
    {
        public string Title { get; set; }

        public SummaryViewModel()
        {
            Title = "SUMMARY";

        }
    }
}