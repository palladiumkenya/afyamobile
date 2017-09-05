using Android.App;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "LiveHTS")]
    public class MainActivity: MvxActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainView);
        }
    }
}