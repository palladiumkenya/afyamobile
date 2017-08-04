using Android.App;
using Android.Content.PM;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Registry", LaunchMode = LaunchMode.SingleTop, ParentActivity = typeof(ClientDashboardActivity),
        Theme = "@android:style/Theme.Material.Light")]
    public class ClientRelationshipsActivity : MvxActivity<ClientRelationshipsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientRelationshipsView);
        }
    }
}