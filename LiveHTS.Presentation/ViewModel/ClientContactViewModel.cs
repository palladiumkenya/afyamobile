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

        public string Description { get; set; }
        public int Step { get; } = 2;

        public bool Validate()
        {
            return true;
        }

        public void Save()
        {
            throw new System.NotImplementedException();
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