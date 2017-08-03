using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientRegistrationViewModel:MvxViewModel,IClientRegistrationViewModel
    {
        private readonly IRegistryService _registryService;
        protected readonly ISettings _settings;
        private Client _client;

        public ClientRegistrationViewModel(ISettings settings, IRegistryService registryService)
        {
            _settings = settings;
            _registryService = registryService;
        }

        public void Init(string id)
        {
            ClearCache();

            if (!string.IsNullOrWhiteSpace(id))
            {
                Client = _registryService.Find(new Guid(id));
            }

            ShowViewModel<ClientDemographicViewModel>();
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
        }
    }
}