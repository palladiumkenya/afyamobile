using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;


namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Client Encounter", LaunchMode = LaunchMode.SingleTop,WindowSoftInputMode = SoftInput.AdjustPan,ParentActivity = typeof(DashboardActivity))]
    public class ClientEncounterActivity : MvxAppCompatActivity<ClientEncounterViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientEncounterView);
            
        }

        public override void OnBackPressed()
        {
            ViewModel.GoBack();
        }
    }
}