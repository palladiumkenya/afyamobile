using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using LiveHTS.Droid.Custom;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;


namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Client Encounter", LaunchMode = LaunchMode.SingleTop,WindowSoftInputMode = SoftInput.AdjustPan,ParentActivity = typeof(DashboardActivity), ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
    public class ClientEncounterActivity : MvxAppCompatActivity<ClientEncounterViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientEncounterView);
            ViewModel.ChangedDate += ViewModel_ChangedDate;

        }
        private void ViewModel_ChangedDate(object sender, Presentation.Events.ChangedDateEvent e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedDate = new TraceDateDTO(e.Id, time.Date);
            }, e.Date);

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
        public override void OnBackPressed()
        {
            ViewModel.GoBack();
        }
    }
}