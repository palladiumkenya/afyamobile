using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using com.refractored.fab;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Stats", LaunchMode = LaunchMode.SingleTop, ParentActivity = typeof(AppDashboardActivity), WindowSoftInputMode = SoftInput.StateHidden)]
    public class UserSummaryActivity : MvxAppCompatActivity<UserSummaryViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserSummaryView);
        }
    }
}