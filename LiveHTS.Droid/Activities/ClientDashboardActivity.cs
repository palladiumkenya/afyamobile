using Android.App;
using Android.Content.PM;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;


namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Client Dashboard", LaunchMode = LaunchMode.SingleTop)]
    public class ClientDashboardActivity:MvxAppCompatActivity<ClientDashboardViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientDashboardView);
        }

        public override void OnBackPressed()
        {
            ViewModel.ShowRegistry();
        }

   
    }
}