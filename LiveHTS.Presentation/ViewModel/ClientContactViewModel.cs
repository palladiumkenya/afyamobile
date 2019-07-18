using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Meta;
using LiveHTS.Core.Model.Meta;
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
    public class ClientContactViewModel : StepViewModel, IClientContactViewModel
    {
        private string _clientInfo;
        private string _telephone;
        private string _landmark;
        private string _personId;
        private string _contactId;
        private string _addressId;
        private IndexClientDTO _indexClientDTO;
        private double? _lat;
        private double? _lng;
        private List<RegionItem> _counties = new List<RegionItem>();
        private RegionItem _selectedCounty;
        private List<RegionItem> _subCounties = new List<RegionItem>();
        private RegionItem _selectedSubCounty;
        private List<RegionItem> _wards = new List<RegionItem>();
        private RegionItem _selectedWard;
        private IMetaService _metaService;

        public IndexClientDTO IndexClientDTO
        {
            get { return _indexClientDTO; }
            set { _indexClientDTO = value; }
        }

        public ClientContactAddressDTO ContactAddress { get; set; }

        public string ClientInfo
        {
            get { return _clientInfo; }
            set
            {
                _clientInfo = value;
                RaisePropertyChanged(() => ClientInfo);
            }
        }

        public string Telephone
        {

            get { return _telephone; }
            set
            {
                _telephone = value;
                RaisePropertyChanged(() => Telephone);
            }
        }

        public string Landmark
        {
            get { return _landmark; }
            set
            {
                _landmark = value;
                RaisePropertyChanged(() => Landmark);
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

        public string ContactId
        {
            get { return _contactId; }
            set
            {
                _contactId = value;
                RaisePropertyChanged(() => ContactId);
            }
        }

        public string AddressId
        {
            get { return _addressId; }
            set
            {
                _addressId = value;
                RaisePropertyChanged(() => AddressId);
            }
        }

        public double? Lat
        {
            get { return _lat; }
            set
            {
                _lat = value;
                RaisePropertyChanged(() => Lat);
            }
        }

        public double? Lng
        {
            get { return _lng; }
            set
            {
                _lng = value;
                RaisePropertyChanged(() => Lng);
            }
        }

        public bool Downloaded { get; set; }

        public List<RegionItem> Counties
        {
            get { return _counties; }
            set
            {
                _counties = value;
                RaisePropertyChanged(() => Counties);
            }
        }

        public RegionItem SelectedCounty
        {
            get { return _selectedCounty; }
            set
            {
                _selectedCounty = value;
                RaisePropertyChanged(() => SelectedCounty);
                GetSubCounties();
            }
        }

        public List<RegionItem> SubCounties
        {
            get { return _subCounties; }
            set
            {
                _subCounties = value;
                RaisePropertyChanged(() => SubCounties);
            }
        }

        public RegionItem SelectedSubCounty
        {
            get { return _selectedSubCounty; }
            set
            {
                _selectedSubCounty = value;
                RaisePropertyChanged(() => SelectedSubCounty);
                GetWards();
            }
        }

        public List<RegionItem> Wards
        {
            get { return _wards; }
            set
            {
                _wards = value;
                RaisePropertyChanged(() => Wards);
            }
        }

        public RegionItem SelectedWard
        {
            get { return _selectedWard; }
            set
            {
                _selectedWard = value;
                RaisePropertyChanged(() => SelectedWard);
            }
        }

        public ClientContactViewModel(IDialogService dialogService, ISettings settings, IMetaService metaService) :
            base(dialogService, settings)
        {
            _metaService = metaService;
            Step = 2;
            Title = "Contacts";
            MovePreviousLabel = "PREV";
            MoveNextLabel = "NEXT";
            Counties = RegionItem.Init("County");
            SubCounties = RegionItem.Init("SubCounty");
            Wards = RegionItem.Init("Ward");
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
                        Title = $"Contacts [{IndexClientDTO.RelType}]";
                }
            }

            Counties = _metaService.GetCounties().ToList();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            var indexJson = _settings.GetValue(nameof(IndexClientDTO), "");
            if (!string.IsNullOrWhiteSpace(indexJson))
            {
                IndexClientDTO = JsonConvert.DeserializeObject<IndexClientDTO>(indexJson);
                if (null != IndexClientDTO)
                    Title = $"Contacts [{IndexClientDTO.RelType}]";
            }

            var countyJson = _settings.GetValue("meta.county", "");
            if (!string.IsNullOrWhiteSpace(countyJson))
            {
                Counties = JsonConvert.DeserializeObject<List<RegionItem>>(countyJson);
            }
        }

        public override bool Validate()
        {
            if (!string.IsNullOrWhiteSpace(Telephone))
            {
                Validator.AddRule(
                    nameof(Telephone),
                    () => RuleResult.Assert(
                        Telephone.Trim().Length >= 9 && isNumeric(Telephone),
                        $"{nameof(Telephone)} is invalid"
                    )
                );
            }

            return base.Validate();
        }

        public override void MoveNext()
        {
            if (Validate())
            {
                ContactAddress = ClientContactAddressDTO.CreateFromView(this);
                var json = JsonConvert.SerializeObject(ContactAddress);
                _settings.AddOrUpdateValue(GetType().Name, json);
                var indexId = null != IndexClientDTO ? IndexClientDTO.Id.ToString() : string.Empty;
                ShowViewModel<ClientProfileViewModel>(new {clientinfo = ClientInfo, indexId = indexId});
            }
            else
            {
                if (null != Errors && Errors.Any())
                    _dialogService.ShowErrorToast(Errors.First().Value);
            }
        }

        public override void MovePrevious()
        {
            ShowViewModel<ClientDemographicViewModel>();
        }

        public override bool CanMoveNext()
        {
            return true;
        }

        public override bool CanMovePrevious()
        {
            return true;
        }

        private void GetSubCounties()
        {
            SubCounties = RegionItem.Init("SubCounty");
            Wards = RegionItem.Init("Ward");
            try
            {
                if (null != SelectedCounty)
                    SubCounties = _metaService.GetSubCounties(SelectedCounty.Id).ToList();
            }
            catch
            {
            }
        }

        private void GetWards()
        {
            Wards = RegionItem.Init("Ward");
            try
            {
                if (null != SelectedSubCounty)
                    Wards = _metaService.GetWards(SelectedSubCounty.Id).ToList();
            }
            catch
            {
            }
        }

        public override void Start()
        {
            base.Start();
            try
            {
                SelectedCounty = Counties.FirstOrDefault(x => x.Id == 0);
                SelectedSubCounty = SubCounties.FirstOrDefault(x => x.Id == 0);
                SelectedWard = Wards.FirstOrDefault(x => x.Id == 0);
            }
            catch
            {
            }
        }

        public override void LoadFromStore(VMStore modelStore)
        {
            try
            {
                ContactAddress = JsonConvert.DeserializeObject<ClientContactAddressDTO>(modelStore.Store);
                PersonId = ContactAddress.PersonId;
                Downloaded = ContactAddress.Downloaded;
                Telephone = ContactAddress.Phone;
                Landmark = ContactAddress.Landmark;
                ContactId = ContactAddress.ContactId;
                AddressId = ContactAddress.AddressId;

                SelectedCounty = Counties.FirstOrDefault(x => x.Id == 0);
                SelectedSubCounty = SubCounties.FirstOrDefault(x => x.Id == 0);
                SelectedWard = Wards.FirstOrDefault(x => x.Id == 0);

                if (ContactAddress.CountyId.HasValue && ContactAddress.CountyId.Value > 0)
                {
                    SelectedCounty = Counties.FirstOrDefault(x => x.Id == ContactAddress.CountyId);
                    GetSubCounties();
                }

                if (ContactAddress.SubCountyId.HasValue && ContactAddress.SubCountyId.Value > 0)
                {
                    SelectedSubCounty = SubCounties.FirstOrDefault(x => x.Id == ContactAddress.SubCountyId);
                    GetWards();
                }

                if (ContactAddress.WardId.HasValue && ContactAddress.WardId.Value > 0)
                {
                    SelectedWard = Wards.FirstOrDefault(x => x.Id == ContactAddress.WardId);
                }



            }
            catch (Exception e)
            {
                Mvx.Error(e.Message);
            }
        }
        private bool isNumeric(string phone)
        {
            return long.TryParse(phone.Trim(), out long n);
        }
    }
}
