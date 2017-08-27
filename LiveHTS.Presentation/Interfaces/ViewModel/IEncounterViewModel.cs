using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IEncounterViewModel:IMvxViewModel
    {
        string Title { get; set; }
    }
}