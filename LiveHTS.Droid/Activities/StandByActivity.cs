﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "StandBy",NoHistory = true)]
    public class StandByActivity : MvxAppCompatActivity<StandByViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.StandByView);
        }
    }
}