using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientContactViewModel : MvxViewModel, IClientContactViewModel
    {
        private string _title;
        private int? _telephone;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public int? Telephone
        {
            get { return _telephone; }
            set
            {
                _telephone = value;
                RaisePropertyChanged(() => Telephone);
            }
        }

        public ClientContactViewModel()
        {
            Title = "Contact";
        }
    }
}