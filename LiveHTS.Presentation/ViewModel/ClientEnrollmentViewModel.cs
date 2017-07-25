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