using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Widget;
using com.refractored;
using LiveHTS.Droid.Adapters;
using LiveHTS.Droid.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.FullFragging.Caching;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Registration", Theme = "@android:style/Theme.Material.Light")]
    public class ClientRegistrationActivity: MvxCachingFragmentActivity<ClientRegistrationViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientRegistrationView);
        }
    }
}