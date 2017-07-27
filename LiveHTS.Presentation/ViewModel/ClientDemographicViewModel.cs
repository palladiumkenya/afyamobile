using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;
using MvvmCross.Platform.Platform;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientDemographicViewModel : MvxViewModel,IClientDemographicViewModel
    {
        private string _title;
        private string _names;
        private string _description;
        private ObservableDictionary<string, string> _errors;
        private readonly IDialogService _dialogService;
        private string _moveNextLabel;
        private string _movePreviousLabel;
        private IMvxCommand _moveNextCommand;
        private IMvxCommand _movePreviousCommand;
        private Person _person;
        private ClientDemographicDTO _clientDemographicDTO=new ClientDemographicDTO();

        public IClientRegistrationViewModel Parent { get; set; }

        public int Step { get; } = 1;
        public string Title
        {
            get { return _title; }
            set{_title = value;RaisePropertyChanged(() => Title);}
        }
        public string Description
        {
            get { return _description; }
            set { _description = value;RaisePropertyChanged(() => Description);}
        }

        public string MoveNextLabel
        {
            get { return _moveNextLabel; }
            set{_moveNextLabel = value; RaisePropertyChanged(() => MoveNextLabel);
            }
        }
        public string MovePreviousLabel
        {
            get { return _movePreviousLabel; }
            set{_movePreviousLabel = value;RaisePropertyChanged(() => MovePreviousLabel);}
        }


        public ValidationHelper Validator { get; }
        public ObservableDictionary<string, string> Errors
        {
            get { return _errors; }
            set{_errors = value;RaisePropertyChanged(() => Errors);}
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


        public List<CustomItem> GenderLists { get; set; }

        public ClientDemographicDTO ClientDemographicDTO
        {
            get { return _clientDemographicDTO; }
            set
            {
                _clientDemographicDTO = value;
                RaisePropertyChanged(() => ClientDemographicDTO);
            }
        }

        

        public ClientDemographicViewModel(IDialogService dialogService)
        {
            GenderLists = CustomLists.Gender;
            _dialogService = dialogService;

            Validator = new ValidationHelper();
            Title = "Demographics";
            MovePreviousLabel = "";
            MoveNextLabel = "NEXT";
        }

       public bool Validate()
        {
            Validator.AddRule(
                nameof(ClientDemographicDTO.FirstName),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(ClientDemographicDTO.FirstName),
                    $"{nameof(ClientDemographicDTO.FirstName)} is required"
                )
            );

            Validator.AddRule(
                nameof(ClientDemographicDTO.LastName),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(ClientDemographicDTO.LastName),
                    $"{nameof(ClientDemographicDTO.LastName)} is required"
                )
            );

            Validator.AddRule(
                nameof(ClientDemographicDTO.Gender),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(ClientDemographicDTO.Gender),
                    $"{nameof(ClientDemographicDTO.Gender)} is required"
                )
            );

            Validator.AddRequiredRule(() => ClientDemographicDTO.BirthDate, $"{nameof(ClientDemographicDTO.BirthDate)} is required");

            if (null != ClientDemographicDTO.BirthDate)
                Validator.AddRule(
                    nameof(ClientDemographicDTO.BirthDate),
                    () => RuleResult.Assert(ClientDemographicDTO.BirthDate.Value <= DateTime.Now, $"{nameof(ClientDemographicDTO.BirthDate)} cannot be future date"));
            
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
            if(Validate())
                ShowViewModel<ClientContactViewModel>();
        }
        private void MovePrevious()
        {
          
        }
        private bool CanMoveNext()
        {
            return true;
        }
        private bool CanMovePrevious()
        {
            return false;
        }
    }
}