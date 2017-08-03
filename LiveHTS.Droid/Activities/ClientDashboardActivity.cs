using Android.App;
using Android.Content.PM;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;


namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Client Dashboard", LaunchMode = LaunchMode.SingleTop,ParentActivity = typeof(RegistryActivity),
        Theme = "@android:style/Theme.Material.Light")]
    public class ClientDashboardActivity:MvxActivity<ClientDashboardViewModel>
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