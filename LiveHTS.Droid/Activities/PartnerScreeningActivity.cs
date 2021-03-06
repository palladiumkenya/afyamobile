﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using com.refractored;
using LiveHTS.Droid.Custom;
using LiveHTS.Droid.Views;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;
using MvvmCross.Platform.Platform;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;




namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Partner Screening", LaunchMode = LaunchMode.SingleTop,WindowSoftInputMode = SoftInput.AdjustPan, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
    public class PartnerScreeningActivity : MvxAppCompatActivity<PartnerScreeningViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PartnerScreeningView);
            ViewModel.ChangedScreeningDate += ViewModel_ChangedScreeningDate;
            ViewModel.ChangedBookingDate += ViewModel_ChangedBookingDate;
        }

        private void ViewModel_ChangedScreeningDate(object sender, Presentation.Events.ChangedDateEvent e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedScreeningDate =new TraceDateDTO(e.Id, time.Date);
            }, e.Date);
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void ViewModel_ChangedBookingDate(object sender, Presentation.Events.ChangedDateEvent e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedBookingDate =new TraceDateDTO(e.Id, time.Date);
            }, e.Date);
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
       
    }
}