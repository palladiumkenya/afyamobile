using LiveHTS.Presentation.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation
{
    public class AppStart: MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            ShowViewModel<MainViewModel>();
        }
    }
}