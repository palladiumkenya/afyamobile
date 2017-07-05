using LiveHTS.Presentation.Interfaces;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class InterviewViewModel : MvxViewModel, IInterviewViewModel
    {
        private string _welcome;

        public string Welcome
        {
            get => _welcome;
            set
            {
                _welcome = value;
                RaisePropertyChanged(() => Welcome); RaisePropertyChanged(() => Greeting);
            }
        }
        public string Greeting => string.IsNullOrWhiteSpace(_welcome) ? string.Empty : $"Karibu {_welcome}";
    }
}