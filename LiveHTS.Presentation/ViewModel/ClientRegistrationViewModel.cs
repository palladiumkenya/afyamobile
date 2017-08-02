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
        }

        public void Init(string mode)
        {
            ClearCache(_settings);
            ShowViewModel<ClientDemographicViewModel>();
        }

        private void ClearCache(ISettings settings)
        {

            if (settings.Contains(nameof(ClientDemographicViewModel)))
                settings.DeleteValue(nameof(ClientDemographicViewModel));

            if (settings.Contains(nameof(ClientContactViewModel)))
                settings.DeleteValue(nameof(ClientContactViewModel));

            if (settings.Contains(nameof(ClientProfileViewModel)))
                settings.DeleteValue(nameof(ClientProfileViewModel));

            if (settings.Contains(nameof(ClientEnrollmentViewModel)))
                settings.DeleteValue(nameof(ClientEnrollmentViewModel));
        }
    }
}