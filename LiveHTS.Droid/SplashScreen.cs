using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid
{
    [Activity(
        Label = "LiveHTS"
        , MainLauncher = true
        , Icon = "@drawable/livehts"
        , Theme = "@android:style/Theme.Material.Light"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
