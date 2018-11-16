using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Sign In", WindowSoftInputMode = SoftInput.StateHidden)]
    public class SignInActivity: MvxAppCompatActivity<SignInViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SignInView);

            ViewModel.LoadDeviceInfo(Android.OS.Build.Serial, Android.OS.Build.Model, Android.OS.Build.Manufacturer);
            try
            {
                ShowVersion();
            }
            catch
            {
            }
        }

        public override void OnBackPressed()
        {
            ViewModel.Quit();
        }

        private void ShowVersion()
        {
            ViewModel.LoadVersion(Application.Context.ApplicationContext.PackageManager
                .GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName);
        }
    }
}