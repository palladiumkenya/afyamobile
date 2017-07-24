﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.FullFragging.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Client Dashboard", LaunchMode = LaunchMode.SingleTop,
        Theme = "@android:style/Theme.Material.Light")]
    public class ClientDashboardActivity:MvxActivity<ClientDashboardViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientDashboardView);
        }
    }
}