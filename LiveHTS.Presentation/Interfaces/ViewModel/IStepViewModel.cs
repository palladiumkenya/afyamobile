using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IStepViewModel : IMvxViewModel
    {
        IClientRegistrationViewModel Parent { get; set; }

        int Step { get; }
        string Title { get; set; }
        string Description { get; set; }
        
        string MoveNextLabel { get; set; }
        string MovePreviousLabel  { get; set; }

        ValidationHelper Validator { get; }
        ObservableDictionary<string, string> Errors { get; set; }

        IMvxCommand MoveNextCommand { get; }
        IMvxCommand MovePreviousCommand { get; }

        bool Validate();
        void Save();
    }
}