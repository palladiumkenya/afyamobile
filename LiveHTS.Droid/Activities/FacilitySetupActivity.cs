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
using MvvmCross.Droid.FullFragging.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "FacilitySetupActivity")]
    public class FacilitySetupActivity : MvxActivity<PracticeViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FacilitySetupView);
            // Create your application here
        }
    }
}