using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientDemographicViewModel : MvxViewModel,IClientDemographicViewModel
    {
        private string _title;
        private string _names;
        private string _description;

        public int Step { get; } = 1;
        public string Title
        {
            get { return _title; }
            set{_title = value; RaisePropertyChanged(() => Title);}
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged(() => Description);}
        }

        public string Names
        {
            get { return _names; }
            set { _names = value; RaisePropertyChanged(() => Names); }
        }
        
        public ClientDemographicViewModel()
        {
            Title = "Demographics";
        }

        public bool Validate()
        {
            return true;
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}