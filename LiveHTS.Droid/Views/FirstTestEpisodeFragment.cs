using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;

namespace LiveHTS.Droid.Views
{
    public class FirstTestEpisodeFragment : MvxFragment<FirstTestEpisodeViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.FirstTestEpisodeView, null);
            var vm = new TestViewModel();
            vm.Parent = ViewModel;

            ViewModel.AddTestCommandAction = () =>
            {
                vm.EditMode = false;

                var dialogFragment = new TestFragment()
                {
                    
                    DataContext = vm
                };
                
                dialogFragment.Show(FragmentManager, "T1Episode01");
            };

            ViewModel.CloseTestCommandAction = () =>
            {
                var frag= FragmentManager.FindFragmentByTag("T1Episode01");
                if(null!=frag)
                    ((TestFragment)frag).Dismiss();
            };

            ViewModel.EditTestCommandAction = () =>
            {
                vm.EditMode = true;
                var dialogFragment = new TestFragment()
                {

                    DataContext = vm
                };

                dialogFragment.Show(FragmentManager, "T1Episode01");
            };

            return view;
        }
    }
}