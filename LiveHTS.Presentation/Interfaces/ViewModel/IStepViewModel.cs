using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.UI;
using MvvmValidation;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IStepViewModel : IMvxViewModel
    {
        IClientRegistrationViewModel Parent { get; set; }
        VMStore ModelStore { get; set; }
        int Step { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        bool ShowId { get; set; }

        string MoveNextLabel { get; set; }
        string MovePreviousLabel  { get; set; }
        
        ValidationHelper Validator { get; }
        ObservableDictionary<string, string> Errors { get; set; }

        IMvxCommand MoveNextCommand { get; }
        IMvxCommand MovePreviousCommand { get; }

        bool Validate();

        void MoveNext();
        void MovePrevious();
        bool CanMoveNext();
        bool CanMovePrevious();
        
        void Save();
        void LoadFromStore(VMStore store);
    }
}