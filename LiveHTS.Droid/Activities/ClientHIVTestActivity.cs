﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;


namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Client HIV Test", LaunchMode = LaunchMode.SingleTop,
        Theme = "@android:style/Theme.Material.Light")]
    public class ClientHIVTestActivity : MvxActivity<ClientHIVTestViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientHIVTestView);
        }
    }
}