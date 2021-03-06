﻿using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Dashboard",LaunchMode = LaunchMode.SingleTop)]
    public class AppDashboardActivity : MvxAppCompatActivity<AppDashboardViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AppDashboardView);
            
        }

        public override void OnBackPressed()
        {
            ViewModel.Quit();
        }
    }
}