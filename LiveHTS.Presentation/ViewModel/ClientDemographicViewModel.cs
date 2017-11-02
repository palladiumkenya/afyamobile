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

        private string _firstName;
        private string _middleName;
        private string _lastName;
        private CustomItem _selectedGender;
        private decimal _age;
        private CustomItem _selectedAgeUnit;
        private DateTime _birthDate;
        
        private string _personId;
        private MvxCommand _showDateDialogCommand;
        private TraceDateDTO _selectedDate;
        private ClientDemographicDTO _demographic;
        private IndexClientDTO _indexClientDTO;

        public ClientDemographicDTO Demographic
        {
            get { return _demographic; }
            set { _demographic = value; RaisePropertyChanged(() => Demographic); }
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
       
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                RaisePropertyChanged(() => BirthDate);             
            }
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
        private void ShowDateDialog()
        {

            ShowDatePicker(Guid.Empty, BirthDate);
        }
        private void UpdatePromiseDate(TraceDateDTO selectedDate)
        {
            BirthDate = selectedDate.EventDate;
        }
        public void ShowDatePicker(Guid refId, DateTime refDate)
        {
            OnChangedDate(new ChangedDateEvent(refId, refDate));
        }
        protected virtual void OnChangedDate(ChangedDateEvent e)
        {
            ChangedDate?.Invoke(this, e);
        }
        public ClientDemographicViewModel(IDialogService dialogService, ISettings settings) : base(dialogService, settings)
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
            if (!string.IsNullOrWhiteSpace(indexJson))
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
            var personAge = PersonAge.Create(Age, SelectedAgeUnit.Value);
            BirthDate = SharedKernel.Custom.Utils.CalculateBirthDate(personAge);
        }

        //TODO: CalculateAge from BirthDate
        public void CalculateAge()
        {      
            if (null != BirthDate)
            {
                var personAge = SharedKernel.Custom.Utils.CalculateAge(BirthDate);
                Age = personAge.Age;
                var ageUnit = AgeUnitOptions.FirstOrDefault(x => x.Value == personAge.AgeUnit);
                SelectedAgeUnit = ageUnit;
            }
        }

        public override void MoveNext()
        {
            if (Validate())
            {
                Demographic =ClientDemographicDTO.CreateFromView(this);
                var json = JsonConvert.SerializeObject(Demographic);
                _settings.AddOrUpdateValue(GetType().Name, json);

                var clientinfo = Demographic.ToString();
                var indexId = null != IndexClientDTO ? IndexClientDTO.Id.ToString() : string.Empty;
                ShowViewModel<ClientContactViewModel>(new { clientinfo = clientinfo, indexId = indexId });
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
                SelectedGender = GenderOptions.FirstOrDefault(x=>x.Value==Demographic.Gender);
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
    }
}