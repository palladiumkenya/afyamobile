using Android.App;
using Android.Content.PM;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Registry",NoHistory = true, ParentActivity = typeof(ClientDashboardActivity))]
    public class ClientRelationshipsActivity : MvxAppCompatActivity<ClientRelationshipsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientRelationshipsView);
        }
    }
}