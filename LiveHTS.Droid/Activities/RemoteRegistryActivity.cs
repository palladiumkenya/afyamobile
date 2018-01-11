using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using com.refractored;
using LiveHTS.Droid.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Remote Registry", LaunchMode = LaunchMode.SingleTop, ParentActivity = typeof(AppDashboardActivity), WindowSoftInputMode = SoftInput.StateHidden)]
    public class RemoteRegistryActivity : MvxAppCompatActivity<RemoteRegistryViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RemoteRegistryView);

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            var fragments = new List<MvxCachingFragmentStatePagerAdapter.FragmentInfo>();

            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo(ViewModel.CohortViewModel.Title, typeof(CohortFragment), ViewModel.CohortViewModel));
            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo(ViewModel.RemoteSearchViewModel.Title, typeof(RemoteSearchFragment), ViewModel.RemoteSearchViewModel));
            

            viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(this, SupportFragmentManager, fragments);
            var pageIndicator = FindViewById<PagerSlidingTabStrip>(Resource.Id.content_frame);
            pageIndicator.SetViewPager(viewPager);
        }
    }
}