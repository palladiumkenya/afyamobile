using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientDemographicViewModel : MvxViewModel,IClientDemographicViewModel
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value; 
                RaisePropertyChanged(() => Title);
            }
        }
    }
}