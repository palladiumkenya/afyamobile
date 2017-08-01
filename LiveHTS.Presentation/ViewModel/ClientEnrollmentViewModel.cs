using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientEnrollmentViewModel : StepViewModel, IClientEnrollmentViewModel
    {
        private ILookupService _lookupService;
        private string _clientInfo;
        private IEnumerable<IdentifierType> _identifierTypes;
        private IdentifierType _selectedIdentifierType;
        private string _identifier;
        private DateTime _registrationDate;

        public ClientEnrollmentDTO Enrollment { get; set; }
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

        public ClientEnrollmentViewModel(IDialogService dialogService, ILookupService lookupService) : base(dialogService)
        {
            Step = 4;
            _lookupService = lookupService;
            Title = "Enrollment";
            MovePreviousLabel = "PREV";
            MoveNextLabel = "SAVE";
            RegistrationDate = DateTime.Today;
        }

        public void Init(string clientinfo)
        {
            ClientInfo = clientinfo;
        }
        public override void Start()
        {
            base.Start();
            IdentifierTypes = _lookupService.GetIdentifierTypes().ToList();
        }

        public override bool Validate()
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

            return base.Validate();
        }

        public override void MoveNext()
        {
            if (Validate())
            {
                _dialogService.Alert("Save Successful","Registration","Ok");    
            }
        }
        public override void MovePrevious()
        {
            ShowViewModel<ClientProfileViewModel>(new { clientinfo = ClientInfo });
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