using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using MvvmCross.Core.ViewModels;
using MvvmValidation;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientDemographicViewModel : StepViewModel, IClientDemographicViewModel
    {
        private string _title;
        private string _description;
        private ObservableDictionary<string, string> _errors;
        private readonly IDialogService _dialogService;
        private string _moveNextLabel;
        private string _movePreviousLabel;
        private IMvxCommand _moveNextCommand;
        private IMvxCommand _movePreviousCommand;
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
        private ClientDemographicDTO _demographic;

        public ClientDemographicDTO Demographic { get; set; }

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
            set
            {
                _middleName = value;
                RaisePropertyChanged(() => MiddleName);
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                RaisePropertyChanged(() => Gender);
            }
        }
        public CustomItem SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                _selectedGender = value;
                Gender = _selectedGender.Value;
                RaisePropertyChanged(() => SelectedGender);
            }
        }
        public decimal Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged(() => Age);
                CalculateBirthDate();
            }
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
        public string BirthDateError
        {
            get { return _birthDateError; }
            set
            {
                _birthDateError = value;
                RaisePropertyChanged(() => BirthDateError);
            }
        }
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                RaisePropertyChanged(() => BirthDate);
                BirthDateError = string.Empty;
                try
                {
                    Errors.Remove("BirthDate");
                }
                catch{}
            }
        }

        public ClientDemographicViewModel(IDialogService dialogService) : base(dialogService)
        {
            Step = 1;
            GenderOptions = CustomLists.GenderList;
            AgeUnitOptions = CustomLists.AgeUnitList;

            SelectedGender = GenderOptions.First();
            SelectedAgeUnit = AgeUnitOptions.First();
            BirthDate = DateTime.Today.AddDays(-1);
            Title = "Demographics";
            MovePreviousLabel = "";
            MoveNextLabel = "NEXT";
        }

        public override bool Validate()
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
                    Age > 0,
                    $"valid {nameof(Age)} is required"
                )
            );

            Validator.AddRequiredRule(() => BirthDate, $"{nameof(BirthDate)} is required");

            if (null != BirthDate)
                Validator.AddRule(
                    nameof(BirthDate),
                    () => RuleResult.Assert(
                        BirthDate.Value < DateTime.Today,
                        $"{nameof(BirthDate)} should be a valid date"));

            return base.Validate();
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
                var personAge = SharedKernel.Custom.Utils.CalculateAge(BirthDate.Value);
                Age = personAge.Age;
                var ageUnit = AgeUnitOptions.FirstOrDefault(x => x.Value == personAge.AgeUnit);
                SelectedAgeUnit = ageUnit;
            }
        }

        public override void MoveNext()
        {
            if (Validate())
            {              
                var demographicJson = JsonConvert.SerializeObject(Demographic);
                ShowViewModel<ClientContactViewModel>(new {demographic = demographicJson});
            }
        }
        public override bool CanMoveNext()
        {
            return true;
        }      
    }
}