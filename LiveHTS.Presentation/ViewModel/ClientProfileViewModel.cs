using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientProfileViewModel : StepViewModel, IClientProfileViewModel
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

        public ClientProfileDTO Profile { get; set; }

        public string ClientInfo
        {
            get { return _clientInfo; }
            set
            {
                _clientInfo = value;
                RaisePropertyChanged(() => ClientInfo);
            }
        }

        public IEnumerable<MaritalStatus> MaritalStatus
        {
            get { return _maritalStatus; }
            set
            {
                _maritalStatus = value;
                RaisePropertyChanged(() => MaritalStatus);
            }
        }

        public IEnumerable<KeyPop> KeyPops
        {
            get { return _keyPops; }
            set
            {
                _keyPops = value;
                RaisePropertyChanged(() => KeyPops);
            }
        }

        public MaritalStatus SelectedMaritalStatus
        {
            get { return _selectedMaritalStatus; }
            set
            {
                _selectedMaritalStatus = value;
                RaisePropertyChanged(() => SelectedMaritalStatus);
            }
        }

        public KeyPop SelectedKeyPop
        {
            get { return _selectedKeyPop; }
            set
            {
                _selectedKeyPop = value;
                RaisePropertyChanged(() => SelectedKeyPop);
                IsOtherKeyPop = _selectedKeyPop.Id.ToLower() == "O".ToLower() ? "visible" : "invisible";
                RaisePropertyChanged(() => IsOtherKeyPop);
            }
        }

        public string IsOtherKeyPop
        {
            get { return _isOtherKeyPop; }
            set
            {
                _isOtherKeyPop = value;
                RaisePropertyChanged(() => IsOtherKeyPop);
            }
        }

        public string OtherKeyPop
        {
            get { return _otherKeyPop; }
            set
            {
                _otherKeyPop = value;
                RaisePropertyChanged(() => OtherKeyPop);
            }
        }

        public ClientProfileViewModel(IDialogService dialogService, ILookupService lookupService) : base(dialogService)
        {
            Step = 3;
            _lookupService = lookupService;

            IsOtherKeyPop = "invisible";
            Title = "Profile";
            MovePreviousLabel = "PREV";
            MoveNextLabel = "NEXT";
        }

        public void Init(string clientinfo)
        {
            ClientInfo = clientinfo;
        }

        public override void Start()
        {
            base.Start();
            MaritalStatus = _lookupService.GetMaritalStatuses().ToList();
            KeyPops = _lookupService.GetKeyPops().ToList();
        }

        public override bool Validate()
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
            return base.Validate();
        }

        public override void MoveNext()
        {
            if (Validate())
                ShowViewModel<ClientEnrollmentViewModel>(new {clientinfo = ClientInfo});
        }
        public override void MovePrevious()
        {
            ShowViewModel<ClientContactViewModel>();
        }
        public override bool CanMoveNext()
        {
            return true;
        }
        public override bool CanMovePrevious()
        {
            return true;
        }
    }
}