using System;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
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

        public ClientContactViewModel(IDialogService dialogService, ISettings settings) : base(dialogService, settings)
        {
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
            }
            catch (Exception e)
            {
                Mvx.Error(e.Message);
            }
        }
    }
}