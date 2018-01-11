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
    [Activity(Label = "Member Tracing", LaunchMode = LaunchMode.SingleTop,WindowSoftInputMode = SoftInput.AdjustPan, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
    public class MemberTracingActivity : MvxCachingFragmentCompatActivity<MemberTracingViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MemberTracingView);

            var vm = new PersonTraceViewModel(); 
            vm.Parent = ViewModel;

            ViewModel.AddTraceCommandAction = () =>
            {
                vm.EditMode = false;

                var dialogFragment = new PersonTraceFragment()
                {

                    DataContext = vm
                };
                
                dialogFragment.Show(SupportFragmentManager, "Trace01");
            };

            ViewModel.CloseTestCommandAction = () =>
            {
                var frag = SupportFragmentManager.FindFragmentByTag("Trace01");
                if (null != frag)
                    ((PersonTraceFragment)frag).Dismiss();
            };

            ViewModel.EditTestCommandAction = () =>
            {
                vm.EditMode = true;
                var dialogFragment = new PersonTraceFragment()
                {

                    DataContext = vm
                };

                dialogFragment.Show(SupportFragmentManager, "Trace01");
            };
        }


    }
}