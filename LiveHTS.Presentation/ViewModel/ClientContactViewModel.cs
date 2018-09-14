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
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientContactViewModel : StepViewModel, IClientContactViewModel
    {
        private string _clientInfo;
        private int? _telephone;
        private string _landmark;
        private string _personId;
        private string _contactId;
        private string _addressId;
        private IndexClientDTO _indexClientDTO;
        private double? _lat;
        private double? _lng;
        private List<Region> _counties=new List<Region>();
        private Region _selectedCounty;
        private List<Region> _subCounties=new List<Region>();
        private Region _selectedSubCounty;
        private List<Region> _wards=new List<Region>();
        private Region _selectedWard;
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
            set { _clientInfo = value; RaisePropertyChanged(() => ClientInfo);}
        }
        public int? Telephone
        {
            get { return _telephone; }
            set { _telephone = value; RaisePropertyChanged(() => Telephone);}
        }
        public string Landmark
        {
            get { return _landmark; }
            set { _landmark = value; RaisePropertyChanged(() => Landmark);}
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
            set { _contactId = value;RaisePropertyChanged(() => ContactId); }
        }

        public string AddressId
        {
            get { return _addressId; }
            set { _addressId = value; RaisePropertyChanged(() => AddressId); }
        }

        public double? Lat
        {
            get { return _lat; }
            set { _lat = value; RaisePropertyChanged(() => Lat); }
        }

        public double? Lng
        {
            get { return _lng; }
            set { _lng = value; RaisePropertyChanged(() => Lng); }
        }

        public List<Region> Counties
        {
            get { return _counties; }
            set
            {
                _counties = value;
                RaisePropertyChanged(() =>Counties);
            }
        }

        public Region SelectedCounty
        {
            get { return _selectedCounty; }
            set
            {
                _selectedCounty = value;
                RaisePropertyChanged(() => SelectedCounty);
                GetSubCounties();
            }
        }

        public List<Region> SubCounties
        {
            get { return _subCounties; }
            set
            {
                _subCounties = value;
                RaisePropertyChanged(() => SubCounties);
            }
        }

        public Region SelectedSubCounty
        {
            get { return _selectedSubCounty; }
            set
            {
                _selectedSubCounty = value;
                RaisePropertyChanged(() => SelectedSubCounty);
                GetWards();
            }
        }

        public List<Region> Wards
        {
            get { return _wards; }
            set
            {
                _wards = value;
                RaisePropertyChanged(() => Wards);
            }
        }

        public Region SelectedWard
        {
            get { return _selectedWard; }
            set
            {
                _selectedWard = value;
                RaisePropertyChanged(() => SelectedWard);
            }
        }

        public ClientContactViewModel(IDialogService dialogService, ISettings settings, IMetaService metaService) : base(dialogService, settings)
        {
            _metaService = metaService;
            Step = 2;
            Title = "Contacts";
            MovePreviousLabel = "PREV";
            MoveNextLabel = "NEXT";
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
                Counties = JsonConvert.DeserializeObject<List<Region>>(countyJson);
            }
        }

        public override void MoveNext()
        {
            if (Validate())
            {
                ContactAddress = ClientContactAddressDTO.CreateFromView(this);
                var json = JsonConvert.SerializeObject(ContactAddress);
                _settings.AddOrUpdateValue(GetType().Name, json);
                var indexId = null != IndexClientDTO ? IndexClientDTO.Id.ToString() : string.Empty;
                ShowViewModel<ClientProfileViewModel>(new {clientinfo = ClientInfo, indexId = indexId });
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

        public void LoadSubCounties(int postion = 0)
        {
            SubCounties = new List<Region>();
            Wards = new List<Region>();
            try
            {
                SelectedCounty = Counties[postion];
               
            }
            catch { }
        }

        public void LoadSubWards(int postion = 0)
        {
            Wards = new List<Region>();
            try
            {
                SelectedSubCounty = SubCounties[postion];
            }
            catch {}
        }

        private void GetSubCounties()
        {
            try
            {
                if (null != SelectedCounty)
                    SubCounties = _metaService.GetSubCounties(SelectedCounty.CountyId).ToList();
            }
            catch { }
        }

        private void GetWards()
        {
            try
            {
                if (null != SelectedSubCounty)
                    Wards = _metaService.GetWards(SelectedSubCounty.SubCountyId).ToList();
            }
            catch { }
        }

        public override void Start()
        {
            base.Start();
            Counties = _metaService.GetCounties().ToList();
            SubCounties = _metaService.GetSubCounties(0).ToList();
            Wards = _metaService.GetWards(0).ToList();

            try
            {
                SelectedCounty = Counties.FirstOrDefault(x => x.CountyId == 0);
                SelectedSubCounty = SubCounties.FirstOrDefault(x => x.SubCountyId == 0);
                SelectedWard = Wards.FirstOrDefault(x => x.WardId == 0);
            }
            catch { }
        }

        public override void LoadFromStore(VMStore modelStore)
        {
            try
            {
                ContactAddress = JsonConvert.DeserializeObject<ClientContactAddressDTO>(modelStore.Store);
                PersonId = ContactAddress.PersonId;
                Telephone = ContactAddress.Phone;
                Landmark = ContactAddress.Landmark;
                ContactId = ContactAddress.ContactId;
                AddressId = ContactAddress.AddressId;
                SelectedCounty = Counties.FirstOrDefault(x => x.CountyId == ContactAddress.CountyId);
                GetSubCounties();
                SelectedSubCounty = SubCounties.FirstOrDefault(x => x.SubCountyId == ContactAddress.SubCountyId);
                GetWards();
                SelectedWard = Wards.FirstOrDefault(x => x.WardId == ContactAddress.WardId);

            }
            catch (Exception e)
            {
                Mvx.Error(e.Message);
            }
        }
    }
}