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
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Registration", Theme = "@android:style/Theme.Material.Light")]
    public class ClientRegistrationActivity: MvxCachingFragmentActivity<ClientRegistrationViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ClientRegistrationView);

            

            Mvx.Trace(MvxTraceLevel.Diagnostic, $"CREATE {nameof(ClientRegistrationActivity)}");
            if(null==bundle)
                return;

            bundle.PutString("App", "LiveHTS");

            if (bundle.ContainsKey("FirstName"))
                Mvx.Trace(MvxTraceLevel.Diagnostic, $"CREATE {nameof(ClientRegistrationActivity)} BUNDLE >> {bundle.GetString("FirstName")}");

            if (bundle.ContainsKey("Landmark"))
                Mvx.Trace(MvxTraceLevel.Diagnostic, $"CREATE {nameof(ClientRegistrationActivity)} BUNDLE >> {bundle.GetString("Landmark")}");

            if (bundle.ContainsKey("OtherKeyPop"))
                Mvx.Trace(MvxTraceLevel.Diagnostic, $"CREATE {nameof(ClientRegistrationActivity)} BUNDLE >> {bundle.GetString("OtherKeyPop")}");

            if (bundle.ContainsKey("Identifier"))
                Mvx.Trace(MvxTraceLevel.Diagnostic, $"CREATE {nameof(ClientRegistrationActivity)} BUNDLE >> {bundle.GetString("Identifier")}");
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            Mvx.Trace(MvxTraceLevel.Diagnostic, $"RESTORE -InstanceState {nameof(ClientRegistrationActivity)}");

            if (null == savedInstanceState)
                return;
            if (savedInstanceState.ContainsKey("FirstName"))
                Mvx.Trace(MvxTraceLevel.Diagnostic, $"RESTORE {nameof(ClientRegistrationActivity)} BUNDLE >> {savedInstanceState.GetString("FirstName")}");

            if (savedInstanceState.ContainsKey("Landmark"))
                Mvx.Trace(MvxTraceLevel.Diagnostic, $"RESTORE {nameof(ClientRegistrationActivity)} BUNDLE >> {savedInstanceState.GetString("Landmark")}");

            if (savedInstanceState.ContainsKey("OtherKeyPop"))
                Mvx.Trace(MvxTraceLevel.Diagnostic, $"RESTORE {nameof(ClientRegistrationActivity)} BUNDLE >> {savedInstanceState.GetString("OtherKeyPop")}");
            
            if (savedInstanceState.ContainsKey("Identifier"))
                Mvx.Trace(MvxTraceLevel.Diagnostic, $"RESTORE {nameof(ClientRegistrationActivity)} BUNDLE >> {savedInstanceState.GetString("Identifier")}");
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            Mvx.Trace(MvxTraceLevel.Diagnostic, $"SAVE -InstanceState {nameof(ClientRegistrationActivity)}");

            if (null == outState)
                return;
            if (outState.ContainsKey("FirstName"))
                outState.PutString("FirstName", outState.GetString("FirstName"));

            if (outState.ContainsKey("Landmark"))
                outState.PutString("Landmark", outState.GetString("Landmark"));

            if (outState.ContainsKey("OtherKeyPop"))
                outState.PutString("OtherKeyPop", outState.GetString("OtherKeyPop"));

            if (outState.ContainsKey("Identifier"))
                outState.PutString("Identifier", outState.GetString("Identifier"));

            

        }
        protected override void OnPause()
        {
            base.OnPause();
            Mvx.Trace(MvxTraceLevel.Diagnostic, $"PAUSE {nameof(ClientRegistrationActivity)}");
        }
    }
}