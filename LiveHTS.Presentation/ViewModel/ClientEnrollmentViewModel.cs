using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientEnrollmentViewModel : MvxViewModel,IClientEnrollmentViewModel
    {
        private string _title;
        private string _serial;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string Description { get; set; }
        public int Step { get; } = 4;
        public bool Validate()
        {
            return true;
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public string Serial
        {
            get { return _serial; }
            set { _serial = value;RaisePropertyChanged(() => Serial); }
        }

        public ClientEnrollmentViewModel()
        {
            Title = "Enrollment";
        }
    }
}