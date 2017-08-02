using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientRegistrationViewModel:MvxViewModel,IClientRegistrationViewModel
    {
        protected readonly ISettings _settings;
        public ClientRegistrationViewModel(ISettings settings)
        {
            _settings = settings;
//            Mvx.TryResolve(out _dialogService);
//            Mvx.TryResolve(out _lookupService);
//            ClientDemographicViewModel = new ClientDemographicViewModel(_dialogService) {Parent = this};
//            ClientContactViewModel = new ClientContactViewModel(_dialogService) {Parent = this};
//            ClientProfileViewModel = new ClientProfileViewModel(_dialogService, _lookupService) {Parent = this};
//            ClientEnrollmentViewModel=   new ClientEnrollmentViewModel(_dialogService,_lookupService) {Parent = this};
            //ShowViewModel<ClientDemographicViewModel>();
        }
        public void Init(string mode)
        {
            if (mode.ToLower() == "new")
            {
                //clear reg cache

                _settings.DeleteValue(nameof(ClientDemographicViewModel));
                _settings.DeleteValue(nameof(ClientContactViewModel));
                _settings.DeleteValue(nameof(ClientProfileViewModel));
                _settings.DeleteValue(nameof(ClientEnrollmentViewModel));
            }
        }
    }
}