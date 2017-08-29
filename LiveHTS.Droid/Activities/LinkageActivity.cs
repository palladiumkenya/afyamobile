using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using com.refractored;
using LiveHTS.Droid.Custom;
using LiveHTS.Droid.Views;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;
using MvvmCross.Platform.Platform;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;




namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Client Linkage", LaunchMode = LaunchMode.SingleTop,WindowSoftInputMode = SoftInput.AdjustPan)]
    public class LinkageActivity : MvxCachingFragmentActivity<LinkageViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LinkageView);
           ViewModel.ChangedDate += ViewModel_ChangedDate;


            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpagerlink);

            var fragments = new List<MvxCachingFragmentStatePagerAdapter.FragmentInfo>();
            
            
            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo(ViewModel.ReferralViewModel.Title, typeof(EncounterFragment), ViewModel.ReferralViewModel));
            fragments.Add(new MvxCachingFragmentStatePagerAdapter.FragmentInfo(ViewModel.LinkedToCareViewModel.Title, typeof(PartnerFragment), ViewModel.LinkedToCareViewModel));
            

            viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(this, SupportFragmentManager, fragments);
            var pageIndicator = FindViewById<PagerSlidingTabStrip>(Resource.Id.content_frame_link);
            pageIndicator.SetViewPager(viewPager);
        }

        private void ViewModel_ChangedDate(object sender, Presentation.Events.ChangedDateEvent e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedDate =new TraceDateDTO(e.Id, time.Date);
            },e.Date);
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
        
    }
}