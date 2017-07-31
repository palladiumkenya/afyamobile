using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
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
        private string _clientInfo;
        private IEnumerable<IdentifierType> _identifierTypes;
        private string _identifier;
        private DateTime _registrationDate;
        private IdentifierType _selectedIdentifierType;
        private ILookupService _lookupService;

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

        public string ClientInfo
        {
            get { return _clientInfo; }
            set { _clientInfo = value;RaisePropertyChanged(() => ClientInfo); }
        }

        public IEnumerable<IdentifierType> IdentifierTypes
        {
            get { return _identifierTypes; }
            set { _identifierTypes = value;RaisePropertyChanged(() => IdentifierTypes); }
        }

        public IdentifierType SelectedIdentifierType
        {
            get { return _selectedIdentifierType; }
            set { _selectedIdentifierType = value;RaisePropertyChanged(() => SelectedIdentifierType); }
        }

        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value;RaisePropertyChanged(() => Identifier); }
        }

        public DateTime RegistrationDate
        {
            get { return _registrationDate; }
            set { _registrationDate = value; RaisePropertyChanged(() => RegistrationDate);}
        }


        public void Init(string clientinfo)
        {
            ClientInfo = clientinfo;
        }

        public ClientEnrollmentViewModel(ILookupService lookupService)
        {
            _lookupService = lookupService;
            _dialogService = Mvx.Resolve<IDialogService>();
            Validator = new ValidationHelper();
            Title = "Enrollment";
            MovePreviousLabel = "PREV";
            MoveNextLabel = "SAVE";
            RegistrationDate=DateTime.Today;
        }

        public override void Start()
        {
            base.Start();
            IdentifierTypes = _lookupService.GetIdentifierTypes().ToList();
        }

        public bool Validate()
        {

            Validator.AddRule(
                nameof(Identifier),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(Identifier),
                    $"{nameof(Identifier)} is required"
                )
            );

            Validator.AddRule(
                nameof(RegistrationDate),
                () => RuleResult.Assert(
                    RegistrationDate <= DateTime.Today,
                    $"{nameof(RegistrationDate)} should not be future date"));

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
            ShowViewModel<ClientProfileViewModel>(new { clientinfo = ClientInfo });
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