using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmValidation;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientProfileViewModel : StepViewModel, IClientProfileViewModel
    {
        private string _clientInfo;
        private string _otherKeyPop;
        private IEnumerable<MaritalStatus> _maritalStatus;
        private IEnumerable<KeyPop> _keyPops;
        private MaritalStatus _selectedMaritalStatus;
        private KeyPop _selectedKeyPop;

        private readonly ILookupService _lookupService;
        private string _isOtherKeyPop;
        private string _clientId;
        private IndexClientDTO _indexClientDTO;
        private IEnumerable<RelationshipType> _relationshipTypes;
        private RelationshipType _selectedRelationshipType;
        private bool _isRelation;
        private string _indexClientName;


        public bool IsRelation
        {
            get { return _isRelation; }
            set
            {
                _isRelation = value;
                RaisePropertyChanged(() => IsRelation);
            }
        }

        public string IndexClientName
        {
            get { return _indexClientName; }
            set
            {
                _indexClientName = value; 
                RaisePropertyChanged(() => IndexClientName);
            }
        }

        public IEnumerable<RelationshipType> RelationshipTypes
        {
            get { return _relationshipTypes; }
            set { _relationshipTypes = value; }
        }

        public RelationshipType SelectedRelationshipType
        {
            get { return _selectedRelationshipType; }
            set
            {
                _selectedRelationshipType = value;
                RaisePropertyChanged(() => SelectedRelationshipType);
            }
        }

        public IndexClientDTO IndexClientDTO
        {
            get { return _indexClientDTO; }
            set { _indexClientDTO = value; }
        }

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

        public string ClientId
        {
            get { return _clientId; }
            set { _clientId = value;RaisePropertyChanged(() => ClientId); }
        }

        public ClientProfileViewModel(IDialogService dialogService, ILookupService lookupService,ISettings settings) : base(dialogService, settings)
        {
            Step = 3;
            _lookupService = lookupService;
            RelationshipTypes = _lookupService.GetRelationshipTypes().ToList();
            IsOtherKeyPop = "invisible";
            Title = "Profile";
            MovePreviousLabel = "PREV";
            MoveNextLabel = "NEXT";
            
        }

        public void Init(string clientinfo, string indexId)
        {
            IndexClientName = string.Empty;
            IsRelation = false;
            ClientInfo = clientinfo;
            if (!string.IsNullOrWhiteSpace(indexId))
            {
                var indexJson = _settings.GetValue(nameof(IndexClientDTO), "");
                if (!string.IsNullOrWhiteSpace(indexJson))
                {
                    IsRelation = true;
                    IndexClientDTO = JsonConvert.DeserializeObject<IndexClientDTO>(indexJson);
                    if (null != IndexClientDTO)
                    {
                        IndexClientName = IndexClientDTO.ToString();
                        Title = $"Profile [{IndexClientDTO.RelType}]";
                        RelationshipTypes = RelationshipTypes.Where(x => x.Description.ToLower() == IndexClientDTO.RelType.ToLower()).ToList();
                    }
                }
            }
        }

        public override void ViewAppeared()
        {
            IndexClientName = string.Empty;
            IsRelation = false;

            base.ViewAppeared();
            var indexJson = _settings.GetValue(nameof(IndexClientDTO), "");
            if (!string.IsNullOrWhiteSpace(indexJson))
            {
                IsRelation = true;
                IndexClientDTO = JsonConvert.DeserializeObject<IndexClientDTO>(indexJson);
                if (null != IndexClientDTO)
                {
                    IndexClientName = IndexClientDTO.ToString();
                    Title = $"Profile [{IndexClientDTO.RelType}]";
                    RelationshipTypes = RelationshipTypes.Where(x => x.Description.ToLower() == IndexClientDTO.RelType.ToLower()).ToList();
                }
            }
        }

        public override void Start()
        {
            base.Start();
            MaritalStatus = _lookupService.GetMaritalStatuses(true).ToList();
            KeyPops = _lookupService.GetKeyPops(true).ToList();
            try
            {
                SelectedMaritalStatus = MaritalStatus.FirstOrDefault(x=>x.Id=="");
                SelectedKeyPop = KeyPops.FirstOrDefault(x => x.Id == "");
            }
            catch 
            {
            }
        }

        public override bool Validate()
        {
            Validator.RemoveAllRules();


            Validator.AddRule(
                "MaritalStatus",
                () => RuleResult.Assert(
                    null!=SelectedMaritalStatus && !string.IsNullOrWhiteSpace(SelectedMaritalStatus.Id),
                    $"Marital Status is required"
                )
            );

            Validator.AddRule(
                "KeyPops",
                () => RuleResult.Assert(
                    null != SelectedKeyPop && !string.IsNullOrWhiteSpace(SelectedKeyPop.Id),
                    $"KeyPops is required"
                )
            );

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
            {
                Profile = ClientProfileDTO.CreateFromView(this);
                var json = JsonConvert.SerializeObject(Profile);
                _settings.AddOrUpdateValue(GetType().Name, json);

                var indexId = null != IndexClientDTO ? IndexClientDTO.Id.ToString() : string.Empty;
                ShowViewModel<ClientEnrollmentViewModel>(new {clientinfo = ClientInfo, indexId = indexId });
            }
        }
        public override void MovePrevious()
        {
            ShowViewModel<ClientContactViewModel>(new { clientinfo = ClientInfo });
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
                Profile = JsonConvert.DeserializeObject<ClientProfileDTO>(modelStore.Store);
                ClientId = Profile.ClientId;
                SelectedMaritalStatus = MaritalStatus.FirstOrDefault(x => x.Id == Profile.MaritalStatus);
                SelectedKeyPop = KeyPops.FirstOrDefault(x => x.Id == Profile.KeyPop);
                OtherKeyPop = Profile.OtherKeyPop;
                SelectedRelationshipType = RelationshipTypes.FirstOrDefault(x=>x.Description.ToLower()==Profile.RelTypeId.ToLower());
            }
            catch (Exception e)
            {
                Mvx.Error(e.Message);
            }
        }

    }
}