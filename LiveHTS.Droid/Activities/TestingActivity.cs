using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using com.refractored;
using LiveHTS.Droid.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V4;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "HIV Tests", NoHistory = true, ParentActivity = typeof(DashboardViewModel))]
    public class TestingActivity : MvxCachingFragmentActivity<TestingViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TestingView);

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            var fragments = new List<MvxCachingFragmentStatePagerAdapter.FragmentInfo>();
            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo("FIRST TEST", typeof(FirstTestEpisodeFragment), ViewModel.FirstTestEpisodeViewModel));
            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo("SECOND TEST", typeof(SecondTestEpisodeFragment), ViewModel.SecondTestEpisodeViewModel));

            viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(this, SupportFragmentManager, fragments);
            var pageIndicator = FindViewById<PagerSlidingTabStrip>(Resource.Id.content_frame);
            pageIndicator.SetViewPager(viewPager);
        }

        public override void OnBackPressed()
        {
            ViewModel.GoBack();
        }
    }
}