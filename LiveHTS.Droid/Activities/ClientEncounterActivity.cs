using Android.App;
using Android.Content.PM;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;


namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Client Encounter", LaunchMode = LaunchMode.SingleTop,
        Theme = "@android:style/Theme.Material.Light")]
    public class ClientEncounterActivity : MvxActivity<ClientEncounterViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientEncounterView);
            ViewModel.ConceptChanged += ViewModel_ConceptChanged;
        }

        private void ViewModel_ConceptChanged(object sender, Presentation.Events.ConceptChangedEvent e)
        {
            throw new System.NotImplementedException();
        }
    }
}