using System;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientRegistrationViewModel:MvxViewModel,IClientRegistrationViewModel
    {
        private readonly IRegistryService _registryService;
        protected readonly ISettings _settings;
        private Client _client;
        private string _indexClientId;


        public ClientRegistrationViewModel(ISettings settings, IRegistryService registryService)
        {
            _settings = settings;
            _registryService = registryService;
        }

        public void Init(string id, string indexId, string reltype,string enroll)
        {
            ClearCache();          

            if (!string.IsNullOrWhiteSpace(indexId) && !string.IsNullOrWhiteSpace(reltype))
            {
               var indexClient = _registryService.Find(new Guid(indexId));
                var indexClientDTO=new IndexClientDTO(new Guid(indexId), reltype);
                if (null != indexClient)
                {
                    indexClientDTO.Names = indexClient.Person.FullName;
                    indexClientDTO.Gender = indexClient.Person.Gender;
                    indexClientDTO.PracticeId = indexClient.PracticeId;
                }
                
                var json = JsonConvert.SerializeObject(indexClientDTO);
                _settings.AddOrUpdateValue(nameof(IndexClientDTO), json);

                ShowViewModel<ClientDemographicViewModel>(new { indexId = indexId });

                return;
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                Client = _registryService.Find(new Guid(id));
                if (!string.IsNullOrWhiteSpace(enroll))
                {
                    Client.PreventEnroll = false;
                    _settings.AddOrUpdateValue("PreventEnroll", "false");
                }
            }

            
            ShowViewModel<ClientDemographicViewModel>();
        }


        public string IndexClientId
        {
            get { return _indexClientId; }
            set { _indexClientId = value; }
        }

        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value;RaisePropertyChanged(() => Client);
                LoadCache();
            }
        }

        public void LoadCache()
        {
            if (null != Client)
            {
                var clientRegistration = ClientRegistrationDTO.Create(Client);
                if (null != clientRegistration)
                {
                    if (null != clientRegistration.ClientDemographic)
                    {
                        var json = JsonConvert.SerializeObject(clientRegistration.ClientDemographic);
                        _settings.AddOrUpdateValue(nameof(ClientDemographicViewModel), json);
                    }
                    if (null != clientRegistration.ClientContactAddress)
                    {
                        var json = JsonConvert.SerializeObject(clientRegistration.ClientContactAddress);
                        _settings.AddOrUpdateValue(nameof(ClientContactViewModel), json);
                    }
                    if (null != clientRegistration.ClientProfile)
                    {
                        var json = JsonConvert.SerializeObject(clientRegistration.ClientProfile);
                        _settings.AddOrUpdateValue(nameof(ClientProfileViewModel), json);
                    }
                    if (null != clientRegistration.ClientEnrollment)
                    {
                        var json = JsonConvert.SerializeObject(clientRegistration.ClientEnrollment);
                        _settings.AddOrUpdateValue(nameof(ClientEnrollmentViewModel), json);
                    }
                }
                _settings.AddOrUpdateValue("PreventEnroll", Client.PreventEnroll.ToString().ToLower());
                _settings.AddOrUpdateValue("PracticeId", Client.PracticeId.ToString());
            }
        }
        public void ClearCache()
        {

            if (_settings.Contains(nameof(ClientDemographicViewModel)))
                _settings.DeleteValue(nameof(ClientDemographicViewModel));

            if (_settings.Contains(nameof(ClientContactViewModel)))
                _settings.DeleteValue(nameof(ClientContactViewModel));

            if (_settings.Contains(nameof(ClientProfileViewModel)))
                _settings.DeleteValue(nameof(ClientProfileViewModel));

            if (_settings.Contains(nameof(ClientEnrollmentViewModel)))
                _settings.DeleteValue(nameof(ClientEnrollmentViewModel));

            if (_settings.Contains(nameof(IndexClientDTO)))
                _settings.DeleteValue(nameof(IndexClientDTO));

            if (_settings.Contains("PreventEnroll"))
                _settings.DeleteValue("PreventEnroll");
            if (_settings.Contains("PracticeId"))
                _settings.DeleteValue("PracticeId");
        }
    }
}