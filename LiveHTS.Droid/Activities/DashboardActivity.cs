using System;
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
    [Activity(Label = "Client Dashboard",LaunchMode = LaunchMode.SingleTop,ParentActivity = typeof(AppDashboardActivity),ConfigurationChanges = ConfigChanges.Orientation|ConfigChanges.ScreenSize|ConfigChanges.KeyboardHidden)]
    public class DashboardActivity : MvxAppCompatActivity<DashboardViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DashboardView);

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            var fragments = new List<MvxCachingFragmentStatePagerAdapter.FragmentInfo>();

            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo(ViewModel.EncounterViewModel.Title, typeof(EncounterFragment), ViewModel.EncounterViewModel));
            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo(ViewModel.FamilyMemberViewModel.Title, typeof(FamilyMemberFragment), ViewModel.FamilyMemberViewModel));
            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo(ViewModel.PartnerViewModel.Title, typeof(PartnerFragment), ViewModel.PartnerViewModel));
            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo(ViewModel.SummaryViewModel.Title, typeof(SummaryFragment), ViewModel.SummaryViewModel));

            viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(this, SupportFragmentManager, fragments);
            var pageIndicator = FindViewById<PagerSlidingTabStrip>(Resource.Id.content_frame);
            pageIndicator.SetViewPager(viewPager);

//            try
//            {
//                viewPager.SetCurrentItem(ViewModel.GetActiveTab(), true);
//            }
//            catch 
//            {
//            }
            
        }

        public override void OnBackPressed()
        {
            ViewModel.GoBack();
        }
    }
}