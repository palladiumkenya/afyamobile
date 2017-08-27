using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class EncounterViewModel :MvxViewModel, IEncounterViewModel
    {
        public string Title { get; set; }

        public EncounterViewModel()
        {
            Title = "ENCOUNTERS";
        }
    }
}