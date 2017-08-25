using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using com.refractored;
using LiveHTS.Droid.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V4;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "HIV Tests", Theme = "@android:style/Theme.Material.Light", NoHistory = true)]
    public class HIVTestActivity : MvxCachingFragmentActivity<HIVTestViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HIVTestView);

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            var fragments = new List<MvxCachingFragmentStatePagerAdapter.FragmentInfo>();
            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo("FIRST TEST", typeof(FirstHIVTestFragment), ViewModel.FirstHIVTestViewModel));
            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo("SECOND TEST", typeof(SecondHIVTestFragment), ViewModel.SecondHIVTestViewModel));

            viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(this, SupportFragmentManager, fragments);
           var pageIndicator = FindViewById<PagerSlidingTabStrip>(Resource.Id.content_frame);
            pageIndicator.SetViewPager(viewPager);
        }
    }
}