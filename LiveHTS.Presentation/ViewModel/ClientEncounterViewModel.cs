using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientEncounterViewModel : MvxViewModel, IClientEncounterViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly ISettings _settings;
        private ClientDTO _clientInfo;

        public ClientDTO ClientDTO
        {
            get { return _clientInfo; }
            set { _clientInfo = value; RaisePropertyChanged(() => ClientDTO); }
        }

        public ClientEncounterViewModel(ISettings settings, IDialogService dialogService)
        {
            _settings = settings;
            _dialogService = dialogService;
        }

        public void Init(string clientId,string formId,string encounterTypeId)
        {
        
        }

        public override void ViewAppeared()
        {
            var client = _settings.GetValue("client", "");

            if (!string.IsNullOrWhiteSpace(client))
            {
                ClientDTO = JsonConvert.DeserializeObject<ClientDTO>(client);
            }
        }
    }
}