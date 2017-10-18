using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmValidation;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientEnrollmentViewModel : StepViewModel, IClientEnrollmentViewModel
    {
        private readonly ILookupService _lookupService;
        private readonly IRegistryService _registryService;
        private string _clientInfo;
        private IEnumerable<IdentifierType> _identifierTypes;
        private IdentifierType _selectedIdentifierType;
        private string _identifier;
        private DateTime _registrationDate;
        private Practice _selectedPractice;
        private IEnumerable<Practice> _practices;
        private string _clientId;
        private string _id;
        private IndexClientDTO _indexClientDTO;

        public IndexClientDTO IndexClientDTO
        {
            get { return _indexClientDTO; }
            set { _indexClientDTO = value; }
        }

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

        public IEnumerable<Practice> Practices
        {
            get { return _practices; }
            set { _practices = value;RaisePropertyChanged(() => Practices); }
        }

        public Practice SelectedPractice
        {
            get { return _selectedPractice; }
            set { _selectedPractice = value; RaisePropertyChanged(() => SelectedPractice);}
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

        public string ClientId
        {
            get { return _clientId; }
            set { _clientId = value;RaisePropertyChanged(() => ClientId); }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(() => Id);}
        }

        public ClientEnrollmentViewModel(IDialogService dialogService, ISettings settings, ILookupService lookupService, IRegistryService registryService) : base(dialogService, settings)
        {
            Step = 4;
            _lookupService = lookupService;
            _registryService = registryService;
            Title = "Enrollment";
            MovePreviousLabel = "PREV";
            MoveNextLabel = "SAVE";
            RegistrationDate = DateTime.Today;
        }

        public void Init(string clientinfo, string indexId)
        {
            ClientInfo = clientinfo;
            if (!string.IsNullOrWhiteSpace(indexId))
            {
                var indexJson = _settings.GetValue(nameof(IndexClientDTO), "");
                if (!string.IsNullOrWhiteSpace(indexJson))
                {
                    IndexClientDTO = JsonConvert.DeserializeObject<IndexClientDTO>(indexJson);
                    if (null != IndexClientDTO)
                        Title = $"Enrollment [{IndexClientDTO.RelType}]";
                }
            }
        }
        public override void ViewAppeared()
        {
            var indexJson = _settings.GetValue(nameof(IndexClientDTO), "");
            if (!string.IsNullOrWhiteSpace(indexJson))
            {
                IndexClientDTO = JsonConvert.DeserializeObject<IndexClientDTO>(indexJson);
                if (null != IndexClientDTO)
                    Title = $"Enrollment [{IndexClientDTO.RelType}]";
            }
        }
        public override void Start()
        {
            IdentifierTypes = _lookupService.GetIdentifierTypes().ToList();
            Practices = _lookupService.GetDefaultPractices().ToList();
            SelectedPractice = _lookupService.GetDefault();
            try
            {
                SelectedIdentifierType = IdentifierTypes.FirstOrDefault();
            }
            catch
            {
                
            }
            
            base.Start();
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

        public override void Save()
        {
            try
            {
                var clientRegistrationDTO = new ClientRegistrationDTO(_settings);
                var client = clientRegistrationDTO.Generate();
                _registryService.SaveOrUpdate(client);
                clientRegistrationDTO.ClearCache(_settings);
                ShowViewModel<DashboardViewModel>(new {id = client.Id.ToString()});
            }
            catch (Exception e)
            {
                Mvx.Error(e.Message);
                _dialogService.Alert($"Could NOT Save ! {e.Message}", "Registration", "Ok");
            }
        }

        public override void MoveNext()
        {
            if (Validate())
            {
                Enrollment = ClientEnrollmentDTO.CreateFromView(this);
                var json = JsonConvert.SerializeObject(Enrollment);
                _settings.AddOrUpdateValue(GetType().Name, json);
                Save();                
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
   
        public override void LoadFromStore(VMStore modelStore)
        {
            try
            {
                Enrollment = JsonConvert.DeserializeObject<ClientEnrollmentDTO>(modelStore.Store);
                ClientId = Enrollment.ClientId;
                SelectedIdentifierType = IdentifierTypes.FirstOrDefault(x => x.Id == Enrollment.IdentifierTypeId);
                Identifier = Enrollment.Identifier;
                RegistrationDate = Enrollment.RegistrationDate;
                Id = Enrollment.Id;
            }
            catch (Exception e)
            {
                Mvx.Error(e.Message);
            }
        }
    }
}