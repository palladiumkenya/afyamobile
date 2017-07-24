using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using com.refractored;
using LiveHTS.Droid.Adapters;
using LiveHTS.Droid.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Registration", Theme = "@android:style/Theme.Material.Light")]
    public class ClientRegistrationActivity: MvxFragmentActivity
    {
        private ViewPager _viewPager;
        private PagerSlidingTabStrip _pageIndicator;
        private MvxViewPagerFragmentAdapter _adapter;

        public new ClientRegistrationViewModel ViewModel
        {
            get { return (ClientRegistrationViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.ClientRegistrationView);

            var fragments = new List<MvxViewPagerFragmentAdapter.FragmentInfo>
            {
                new MvxViewPagerFragmentAdapter.FragmentInfo
                {
                    FragmentType = typeof(DemographicFragment),
                    Title = "First Fragment",
                    ViewModel = ViewModel.Demographic
                },
                new MvxViewPagerFragmentAdapter.FragmentInfo
                {
                    FragmentType = typeof(ContactFragment),
                    Title = "Second Fragment",
                    ViewModel = ViewModel.Contact
                },
                new MvxViewPagerFragmentAdapter.FragmentInfo
                {
                    FragmentType = typeof(ProfileFragment),
                    Title = "Third Fragment",
                    ViewModel = ViewModel.Profile
                },
                new MvxViewPagerFragmentAdapter.FragmentInfo
                {
                    FragmentType = typeof(EnrollmentFragment),
                    Title = "Third Fragment",
                    ViewModel = ViewModel.Enrollment
                }
            };

            _viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            _adapter = new MvxViewPagerFragmentAdapter(this, SupportFragmentManager, fragments);
            _viewPager.Adapter = _adapter;

            _pageIndicator = FindViewById<PagerSlidingTabStrip>(Resource.Id.viewPagerIndicator);

            _pageIndicator.SetViewPager(_viewPager);

        }
    }
}