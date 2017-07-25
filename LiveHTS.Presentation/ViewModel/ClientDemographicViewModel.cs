using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientDemographicViewModel : MvxViewModel,IClientDemographicViewModel
    {
        private string _title;
        private string _names;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value; 
                RaisePropertyChanged(() => Title);
            }
        }

        public string Names
        {
            get { return _names; }
            set { _names = value; RaisePropertyChanged(() => Names);}
        }

        public ClientDemographicViewModel()
        {
            Title = "Demographics";
        }
    }
}