using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IStepViewModel : IMvxViewModel
    {
        string Title { get; set; }
        string Description { get; set; }
        int Step { get; }
        bool Validate();
        void Save();
    }
}