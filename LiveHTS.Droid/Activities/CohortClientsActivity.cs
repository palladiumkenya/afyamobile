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
    [Activity(Label = "Cohort Clients", LaunchMode = LaunchMode.SingleTop, ParentActivity = typeof(AppDashboardActivity), WindowSoftInputMode = SoftInput.StateHidden)]
    public class CohortClientsActivity : MvxAppCompatActivity<CohortClientsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CohortClientsView);

            var listView = FindViewById<ListView>(Resource.Id.list);
            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.AttachToListView(listView);
        }
    }
}