using LiveHTS.Presentation.Interfaces;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class CounsellingViewModel : MvxViewModel, ICounsellingViewModel
    {

        private string _form;

        public string Form
        {
            get { return _form; }
            set
            {
                _form = value;
                RaisePropertyChanged(() => Form);
            }
        }

        public void Init(string form)
        {
            Form = form;
        }

        public override void Start()
        {
            base.Start();
            ShowViewModel<InterviewViewModel>();
        }
    }
}