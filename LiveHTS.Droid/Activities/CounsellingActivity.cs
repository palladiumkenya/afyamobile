using Android.App;
using Android.OS;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.FullFragging.Caching;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "LiveHTS Counselling")]
    public class CounsellingActivity:MvxCachingFragmentActivity<CounsellingViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CounsellingView);
        }
    }
}