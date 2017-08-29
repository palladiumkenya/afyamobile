using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using LiveHTS.Droid.Custom;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;
using MvvmCross.Platform.Platform;


namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Client Linkage", LaunchMode = LaunchMode.SingleTop,WindowSoftInputMode = SoftInput.AdjustPan)]
    public class LinkageActivity : MvxActivity<LinkageViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LinkageView);
           ViewModel.ChangedDate += ViewModel_ChangedDate; 
        }

        private void ViewModel_ChangedDate(object sender, Presentation.Events.ChangedDateEvent e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedDate =new TraceDateDTO(e.Id, time.Date);
            },e.Date);
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
        
    }
}