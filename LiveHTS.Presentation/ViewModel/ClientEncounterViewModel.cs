using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientEncounterViewModel : MvxViewModel, IClientEncounterViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly ISettings _settings;
        private string _clientInfo;

        public string ClientInfo
        {
            get { return _clientInfo; }
            set { _clientInfo = value; RaisePropertyChanged(() => ClientInfo); }
        }

        public ClientEncounterViewModel(ISettings settings, IDialogService dialogService)
        {
            _settings = settings;
            _dialogService = dialogService;
        }
    }
}