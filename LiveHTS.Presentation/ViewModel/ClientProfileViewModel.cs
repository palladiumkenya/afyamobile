using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientProfileViewModel : MvxViewModel,IClientProfileViewModel
    {
        private string _title;
        private string _description;
        private ObservableDictionary<string, string> _errors;
        private readonly IDialogService _dialogService;
        private string _moveNextLabel;
        private string _movePreviousLabel;
        private IMvxCommand _moveNextCommand;
        private IMvxCommand _movePreviousCommand;
        
        private string _clientInfo;
        private string _otherKeyPop;
        private IEnumerable<MaritalStatus> _maritalStatus;
        private IEnumerable<KeyPop> _keyPops;
        private MaritalStatus _selectedMaritalStatus;
        private KeyPop _selectedKeyPop;

        private readonly ILookupService _lookupService;
        private string _isOtherKeyPop;


        public IClientRegistrationViewModel Parent { get; set; }

        public int Step { get; } = 3;
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

        public ClientContactAddressDTO ContactAddress { get; }

        public string ClientInfo
        {
            get { return _clientInfo; }
            set { _clientInfo = value;RaisePropertyChanged(() => ClientInfo); }
        }

        public IEnumerable<MaritalStatus> MaritalStatus
        {
            get { return _maritalStatus; }
            set { _maritalStatus = value;RaisePropertyChanged(() => MaritalStatus); }
        }

        public IEnumerable<KeyPop> KeyPops
        {
            get { return _keyPops; }
            set { _keyPops = value;RaisePropertyChanged(() => KeyPops); }
        }

        public MaritalStatus SelectedMaritalStatus
        {
            get { return _selectedMaritalStatus; }
            set { _selectedMaritalStatus = value;RaisePropertyChanged(() => SelectedMaritalStatus); }
        }

        public KeyPop SelectedKeyPop
        {
            get { return _selectedKeyPop; }
            set
            {
                _selectedKeyPop = value;
                RaisePropertyChanged(() => SelectedKeyPop);
                IsOtherKeyPop = _selectedKeyPop.Id.ToLower() == "O".ToLower()? "visible" : "invisible";
                RaisePropertyChanged(() => IsOtherKeyPop);
            }
        }

        public string IsOtherKeyPop
        {
            get { return _isOtherKeyPop; }
            set { _isOtherKeyPop = value;RaisePropertyChanged(() => IsOtherKeyPop); }
        }


        public string OtherKeyPop
        {
            get { return _otherKeyPop; }
            set { _otherKeyPop = value;RaisePropertyChanged(() => OtherKeyPop); }
        }


        public void Init(string clientinfo)
        {
            ClientInfo = clientinfo;
        }

        public ClientProfileViewModel(ILookupService lookupService)
        {
            IsOtherKeyPop = "invisible";
            _lookupService = lookupService;
            _dialogService = Mvx.Resolve<IDialogService>();

            Validator = new ValidationHelper();
            Title = "Profile";
            MovePreviousLabel = "PREV";
            MoveNextLabel = "NEXT";
        }

        public override void Start()
        {
            base.Start();
            MaritalStatus = _lookupService.GetMaritalStatuses().ToList();
            KeyPops = _lookupService.GetKeyPops().ToList();
        }


        public bool Validate()
        {
            Validator.RemoveAllRules();
            
            if (IsOtherKeyPop.ToLower() == "visible")
            {
                Validator.AddRule(
                    nameof(OtherKeyPop),
                    () => RuleResult.Assert(
                        !string.IsNullOrWhiteSpace(OtherKeyPop),
                        $"{nameof(OtherKeyPop)} has to be specified"
                    )
                );
            }
            else
            {
                try
                {
                    Errors.Remove("OtherKeyPop");
                }
                catch
                {
                }
            }

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
                ShowViewModel<ClientEnrollmentViewModel>(new { clientinfo = ClientInfo });
        }

        private void MovePrevious()
        {
            ShowViewModel<ClientContactViewModel>();
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