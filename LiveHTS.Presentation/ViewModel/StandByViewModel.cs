using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class StandByViewModel : MvxViewModel, IStandByViewModel
    {
        private string _waitInfo;

        public string WaitInfo
        {
            get { return _waitInfo; }
            set
            {
                _waitInfo = value;
                RaisePropertyChanged(() => WaitInfo);
            }
        }

        public void Init(string id, string callerId, string mode)
        {
            if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(callerId) && string.IsNullOrWhiteSpace(mode))
            {
                Close(this);
            }

            ShowViewModel<DashboardViewModel>(new { id = id, callerId = callerId, mode = mode });

        }
    }
}