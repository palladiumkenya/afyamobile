using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientEnrollmentViewModel : MvxViewModel,IClientEnrollmentViewModel
    {
        private string _title;
        private string _description;
        private ObservableDictionary<string, string> _errors;
        private readonly IDialogService _dialogService;
        private string _moveNextLabel;
        private string _movePreviousLabel;
        private IMvxCommand _moveNextCommand;
        private IMvxCommand _movePreviousCommand;        
        private string _serial;

        public IClientRegistrationViewModel Parent { get; set; }

        public int Step { get; } = 4;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title); }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged(() => Description); }
        }

        public string MoveNextLabel
        {
            get { return _moveNextLabel; }
            set
            {
                _moveNextLabel = value; RaisePropertyChanged(() => MoveNextLabel);
            }
        }
        public string MovePreviousLabel
        {
            get { return _movePreviousLabel; }
            set { _movePreviousLabel = value; RaisePropertyChanged(() => MovePreviousLabel); }
        }


        public ValidationHelper Validator { get; }
        public ObservableDictionary<string, string> Errors
        {
            get { return _errors; }
            set { _errors = value; RaisePropertyChanged(() => Errors); }
        }

        public IMvxCommand MoveNextCommand
        {
            get
            {
                _moveNextCommand = _moveNextCommand ?? new MvxCommand(MoveNext, CanMoveNext);
                return _moveNextCommand;
            }
        }
        public IMvxCommand MovePreviousCommand
        {
            get
            {
                _movePreviousCommand = _movePreviousCommand ?? new MvxCommand(MovePrevious, CanMovePrevious);
                return _movePreviousCommand;
            }
        }

        public string Serial
        {
            get { return _serial; }
            set { _serial = value; RaisePropertyChanged(() => Serial);}
        }

        public ClientEnrollmentViewModel()
        {
            _dialogService = Mvx.Resolve<IDialogService>();
            Validator = new ValidationHelper();
            Title = "Enrollment";
            MovePreviousLabel = "PREV";
            MoveNextLabel = "SAVE";
        }

        public bool Validate()
        {
            Validator.AddRequiredRule(() => Serial, $"Serial is required");

            var result = Validator.ValidateAll();

            Errors = result.AsObservableDictionary();
            
            return result.IsValid;
        }
        public void Save()
        {
            throw new System.NotImplementedException();
        }

        private void MoveNext()
        {
            if (Validate())
            {
                _dialogService.Alert("Save Successful","Registration","Ok");    
            }
        }
        private void MovePrevious()
        {
            ShowViewModel<ClientProfileViewModel>();
        }
        private bool CanMoveNext()
        {
            return true;
        }
        private bool CanMovePrevious()
        {
            return true;
        }

        
    }
}