using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using Microsoft.VisualBasic.CompilerServices;
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
        private string _description;
        private ObservableDictionary<string, string> _errors;
        private readonly IDialogService _dialogService;
        private string _moveNextLabel;
        private string _movePreviousLabel;
        private IMvxCommand _moveNextCommand;
        private IMvxCommand _movePreviousCommand;
        private ClientDemographicDTO _clientDemographicDTO=new ClientDemographicDTO();
        private CustomItem _selectedGender;
        private List<CustomItem> _genderOptions;
        private decimal _age;
        private List<CustomItem> _ageUnitOptions;
        private CustomItem _selectedAgeUnit;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _gender;
        private DateTime? _birthDate;
        private string _birthDateError;


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


        public List<CustomItem> GenderOptions
        {
            get { return _genderOptions; }
            set
            {
                _genderOptions = value;
                RaisePropertyChanged(() => GenderOptions);
            }
        }

        public List<CustomItem> AgeUnitOptions
        {
            get { return _ageUnitOptions; }
            set
            {
                _ageUnitOptions = value; 
                RaisePropertyChanged(() => AgeUnitOptions);
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; RaisePropertyChanged(() => MiddleName); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; RaisePropertyChanged(() => LastName); }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; RaisePropertyChanged(() => Gender); }
        }

        public decimal Age
        {
            get { return _age; }
            set
            {
                _age = value;RaisePropertyChanged(() => Age);
                CalculateBirthDate();
            }
        }

        public string BirthDateError
        {
            get { return _birthDateError; }
            set { _birthDateError = value; RaisePropertyChanged(() => BirthDateError);}
        }

        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                RaisePropertyChanged(() => BirthDate);
                BirthDateError=string.Empty;
                try
                {
                    Errors.Remove("BirthDate");
                }
                catch 
                {
                    
                }
                
                //CalculateAge();
            }
        }

        public CustomItem SelectedGender
        {
            get { return _selectedGender; }
            set { _selectedGender = value;
                Gender = _selectedGender.Value;
                RaisePropertyChanged(() => SelectedGender); }
        }

        public CustomItem SelectedAgeUnit
        {
            get { return _selectedAgeUnit; }
            set
            {
                _selectedAgeUnit = value;
                RaisePropertyChanged(() => SelectedAgeUnit);
                CalculateBirthDate();
            }
        }

        public void CalculateBirthDate()
        {
            var personAge = PersonAge.Create(Age, SelectedAgeUnit.Value);
            BirthDate = SharedKernel.Custom.Utils.CalculateBirthDate(personAge);
        }

        public void CalculateAge()
        {
            if (null != BirthDate)
            {
              var personAge=   SharedKernel.Custom.Utils.CalculateAge(BirthDate.Value);
                Age = personAge.Age;
                var ageUnit = AgeUnitOptions.FirstOrDefault(x => x.Value == personAge.AgeUnit);
                SelectedAgeUnit = ageUnit;
            }
        }


        public ClientDemographicViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            
            GenderOptions = CustomLists.GenderList;
            AgeUnitOptions = CustomLists.AgeUnitList;

            SelectedGender = GenderOptions.First();
            SelectedAgeUnit = AgeUnitOptions.First();
            BirthDate=DateTime.Today.AddDays(-1);
            Validator = new ValidationHelper();
            Title = "Demographics";
            MovePreviousLabel = "";
            MoveNextLabel = "NEXT";
        }

       public bool Validate()
        {
            Validator.AddRule(
                nameof(FirstName),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(FirstName),
                    $"{nameof(FirstName)} is required"
                )
            );

            Validator.AddRule(
                nameof(LastName),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(LastName),
                    $"{nameof(LastName)} is required"
                )
            );

            Validator.AddRule(
                nameof(Gender),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(Gender),
                    $"{nameof(Gender)} is required"
                )
            );

            Validator.AddRule(
                nameof(Age),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(Age.ToString()),
                    $"{nameof(Age)} is required"
                )
            );

            Validator.AddRule(
                nameof(Age),
                () => RuleResult.Assert(
                    Age>0,
                    $"valid {nameof(Age)} is required"
                )
            );

            Validator.AddRequiredRule(() => BirthDate, $"{nameof(BirthDate)} is required");

            if (null != BirthDate)
                Validator.AddRule(
                    nameof(BirthDate),
                    () => RuleResult.Assert(
                        BirthDate.Value < DateTime.Today,
                        $"{nameof(BirthDate)} not a valid date"));
            
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