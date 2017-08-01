using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientContactViewModel : StepViewModel, IClientContactViewModel
    {
        private string _clientInfo;
        private int? _telephone;
        private string _landmark;

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

        public ClientContactViewModel(IDialogService dialogService) : base(dialogService)
        {
            Step = 2;
            Title = "Contacts";
            MovePreviousLabel = "PREV";
            MoveNextLabel = "NEXT";
        }

        public void Init(string demographic)
        {
            var demographicInfo = JsonConvert.DeserializeObject<ClientDemographicDTO>(demographic);
            ClientInfo = demographicInfo.ToString();
        }

        public override void MoveNext()
        {
            if (Validate())
                 ShowViewModel<ClientProfileViewModel>(new { clientinfo = ClientInfo });
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
    }
}