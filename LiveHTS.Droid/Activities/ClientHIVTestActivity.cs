using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using LiveHTS.Droid.Custom;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;
using MvvmCross.Platform.Platform;


namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Client HIV Test", LaunchMode = LaunchMode.SingleTop,
        Theme = "@android:style/Theme.Material.Light", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class ClientHIVTestActivity : MvxActivity<ClientHIVTestViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientHIVTestView);
            ViewModel.ChangedDate += ViewModel_ChangedDate; ;
        }

        private void ViewModel_ChangedDate(object sender, Presentation.Events.ChangedDateEvent e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedDate =new ExpiryDateDTO(e.Id, time.Date);
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
        
    }
}