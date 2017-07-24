using Android.App;
using Android.Content.PM;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Dashboard",LaunchMode = LaunchMode.SingleTop,
        Theme = "@android:style/Theme.Material.Light")]
    public class AppDashboardActivity : MvxActivity<AppDashboardViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AppDashboardView);
        }
    }
}