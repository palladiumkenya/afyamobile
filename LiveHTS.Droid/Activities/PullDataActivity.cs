using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Pull Data")]
    public class PullDataActivity  : MvxAppCompatActivity<PullDataViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PullDataView);
        }
    }
}