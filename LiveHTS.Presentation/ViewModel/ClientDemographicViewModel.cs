using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.UI;
using MvvmValidation;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientDemographicViewModel : StepViewModel, IClientDemographicViewModel
    {
        private List<CustomItem> _genderOptions;
        private List<CustomItem> _ageUnitOptions;
        private bool _dateHasFoucus = false;
        private bool _ageCalculated = false;
        private bool _ageUnitCalculated = false;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private CustomItem _selectedGender;
        private decimal _age;
        private CustomItem _selectedAgeUnit;
        private DateTime _birthDate;

        private string _personId;
        private MvxCommand _showDateDialogCommand;
        private MvxCommand _showAgeDialogCommand;
        private TraceDateDTO _selectedDate;
        private ClientDemographicDTO _demographic;
        private IndexClientDTO _indexClientDTO;
        private string _nickName;

        public ClientDemographicDTO Demographic
        {
            get { return _demographic; }
            set
            {
                _demographic = value;
                RaisePropertyChanged(() => Demographic);
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

        public IndexClientDTO IndexClientDTO
        {
            get { return _indexClientDTO; }
            set { _indexClientDTO = value; }
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

        public string NickName
        {
            get { return _nickName; }
            set
            {
                _nickName = value;
                RaisePropertyChanged(() => NickName);
            }
        }

        public CustomItem SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                _selectedGender = value;
                RaisePropertyChanged(() => SelectedGender);
            }
        }

        public decimal Age
        {
            get { return _age; }
            set
            {
                bool hasChanged = AgeHasChanged(_age, value);
                _age = value;
                RaisePropertyChanged(() => Age);
                if (!_dateHasFoucus && !_ageCalculated && hasChanged)
                    CalculateBirthDate();
            }
        }

        public CustomItem SelectedAgeUnit
        {
            get { return _selectedAgeUnit; }
            set
            {
                bool hasChanged = AgeUnitHasChanged(_selectedAgeUnit, value);
                _selectedAgeUnit = value;
                RaisePropertyChanged(() => SelectedAgeUnit);
                if (!_dateHasFoucus && !_ageUnitCalculated && hasChanged)
                    CalculateBirthDate();
            }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                bool hasChanged = BirthDateHasChanged(_birthDate, value);
                _birthDate = value;
                RaisePropertyChanged(() => BirthDate);
                if (_dateHasFoucus || isDefaultAge())
                    CalculateAge();
            }
        }

        private bool isDefaultAge()
        {
            return BirthDate.Date < DateTime.Today.Date && Age == 0;
        }

        public string PersonId
        {
            get { return _personId; }
            set
            {
                _personId = value;
                RaisePropertyChanged(() => PersonId);
            }
        }

        public event EventHandler<ChangedDateEvent> ChangedDate;

        public TraceDateDTO SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
                UpdatePromiseDate(SelectedDate);
            }
        }

        public IMvxCommand ShowDateDialogCommand
        {
            get
            {
                _showDateDialogCommand = _showDateDialogCommand ?? new MvxCommand(ShowDateDialog);
                return _showDateDialogCommand;
            }
        }

        public IMvxCommand ShowAgeDialogCommand
        {
            get
            {
                _showAgeDialogCommand = _showAgeDialogCommand ?? new MvxCommand(ShowAgeDialog);
                return _showAgeDialogCommand;
            }
        }

        private void ShowDateDialog()
        {
            _dateHasFoucus = true;
            _ageCalculated = false;
            ShowDatePicker(Guid.Empty, BirthDate);
        }

        private void ShowAgeDialog()
        {
            _ageCalculated = true;
        }

        private void UpdatePromiseDate(TraceDateDTO selectedDate)
        {
            BirthDate = selectedDate.EventDate;
            _dateHasFoucus = false;
        }

        public void ShowDatePicker(Guid refId, DateTime refDate)
        {
            OnChangedDate(new ChangedDateEvent(refId, refDate));
        }

        protected virtual void OnChangedDate(ChangedDateEvent e)
        {
            ChangedDate?.Invoke(this, e);
        }

        public ClientDemographicViewModel(IDialogService dialogService, ISettings settings) : base(dialogService,
            settings)
        {
            Step = 1;
            GenderOptions = CustomLists.GenderList;
            AgeUnitOptions = CustomLists.AgeUnitList;

            SelectedGender = GenderOptions.First();
            SelectedAgeUnit = AgeUnitOptions.First();

            BirthDate = DateTime.Today; //.AddDays(-1);
            Title = "Demographics";
            MovePreviousLabel = "";
            MoveNextLabel = "NEXT";
            Age = 0;
            SelectedAgeUnit = AgeUnitOptions.First();
        }

        public void Init(string indexId)
        {
            if (!string.IsNullOrWhiteSpace(indexId))
            {
                var indexJson = _settings.GetValue(nameof(IndexClientDTO), "");
                if (!string.IsNullOrWhiteSpace(indexJson))
                {
                    IndexClientDTO = JsonConvert.DeserializeObject<IndexClientDTO>(indexJson);
                    if (null != IndexClientDTO)
                        Title = $"Demographics [{IndexClientDTO.RelType}]";
                }
            }
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            var indexJson = _settings.GetValue(nameof(IndexClientDTO), "");
            if (IndexClientDTO == null && !string.IsNullOrWhiteSpace(indexJson))
            {
                IndexClientDTO = JsonConvert.DeserializeObject<IndexClientDTO>(indexJson);
                if (null != IndexClientDTO)
                    Title = $"Demographics [{IndexClientDTO.RelType}]";
            }
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
                nameof(Age),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(Age.ToString()),
                    $"{nameof(Age)} is required"
                )
            );

            Validator.AddRequiredRule(() => BirthDate, $"{nameof(BirthDate)} is required");

            Validator.AddRule(
                nameof(BirthDate),
                () => RuleResult.Assert(
                    BirthDate < DateTime.Today,
                    $"{nameof(BirthDate)} should be a valid date"));

            return base.Validate();
        }

        public void CalculateBirthDate()
        {
            try
            {
                var personAge = PersonAge.Create(Age, SelectedAgeUnit.Value);
                BirthDate = SharedKernel.Custom.Utils.CalculateBirthDate(personAge);
            }
            catch (Exception ex)
            {

            }
        }

        //TODO: CalculateAge from BirthDate
        public void CalculateAge()
        {
            _ageCalculated = true;

            PersonAge personAge = null;
            try
            {
                personAge = SharedKernel.Custom.Utils.CalculateAge(BirthDate);
            }
            catch (Exception ex)
            {

            }

            if (null == personAge)
                return;

            Age = personAge.Age;
            _ageCalculated = false;
            var ageUnit = AgeUnitOptions.FirstOrDefault(x => x.Value == personAge.AgeUnit);
            _ageUnitCalculated = true;
            SelectedAgeUnit = ageUnit;
            _ageUnitCalculated = false;
        }

        public override void MoveNext()
        {
            if (Validate())
            {
                Demographic = ClientDemographicDTO.CreateFromView(this);
                var json = JsonConvert.SerializeObject(Demographic);
                _settings.AddOrUpdateValue(GetType().Name, json);

                var clientinfo = Demographic.ToString();
                var indexId = null != IndexClientDTO ? IndexClientDTO.Id.ToString() : string.Empty;
                ShowViewModel<ClientContactViewModel>(new {clientinfo = clientinfo, indexId = indexId});
            }
            else
            {
                if (null != Errors && Errors.Any())
                    _dialogService.ShowErrorToast(Errors.First().Value);
            }
        }

        public override bool CanMoveNext()
        {
            return true;
        }

        public override void LoadFromStore(VMStore modelStore)
        {
            try
            {
                Demographic = JsonConvert.DeserializeObject<ClientDemographicDTO>(modelStore.Store);
                PersonId = Demographic.PersonId;
                FirstName = Demographic.FirstName;
                MiddleName = Demographic.MiddleName;
                LastName = Demographic.LastName;
                NickName = Demographic.NickName;
                SelectedGender = GenderOptions.FirstOrDefault(x => x.Value == Demographic.Gender);
                Age = Demographic.Age;
                if (!string.IsNullOrWhiteSpace(Demographic.AgeUnit))
                    SelectedAgeUnit = AgeUnitOptions.FirstOrDefault(x => x.Value == Demographic.AgeUnit);
                BirthDate = Demographic.BirthDate;
            }
            catch (Exception e)
            {
                Mvx.Error(e.Message);
            }
        }

        bool AgeHasChanged(decimal oldAge, decimal newAge)
        {
            return oldAge != newAge;
        }

        bool AgeUnitHasChanged(CustomItem oldUnit, CustomItem newUnit)
        {
            return null != oldUnit && !oldUnit.Equals(newUnit);
        }

        bool BirthDateHasChanged(DateTime oldBirth, DateTime newBirth)
        {
            return oldBirth.Date != newBirth.Date;
        }
    }
}
