using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientProfileViewModel : MvxViewModel,IClientProfileViewModel
    {
        private string _title;
        private string _keyPop;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string KeyPop
        {
            get { return _keyPop; }
            set { _keyPop = value;RaisePropertyChanged(() => KeyPop); }
        }

        public ClientProfileViewModel()
        {
            Title = "Profile";
        }
    }
}