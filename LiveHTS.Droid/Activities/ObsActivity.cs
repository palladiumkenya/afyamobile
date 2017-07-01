using Android.App;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "LiveHTS")]
    public class ObsActivity: MvxActivity<ObsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ObsActivity);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            IMvxSavedStateConverter converter;
            if (!Mvx.TryResolve(out converter))
            {
                MvxTrace.Warning("Saved state converter not available - saving state will be hard");
            }
            else
            {
                if (savedInstanceState != null)
                {
                    var mvxBundle = converter.Read(savedInstanceState);
                    this.ViewModel.ReloadState(mvxBundle);
                }
            }
            base.OnRestoreInstanceState(savedInstanceState);
        }
    }
}