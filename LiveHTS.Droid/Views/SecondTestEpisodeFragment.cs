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
    public class SecondTestEpisodeFragment : MvxFragment<SecondTestEpisodeViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.SecondTestEpisodeView, null);

            ViewModel.AddTestCommandAction = () => {
                var dialogFragment = new TestFragment()
                {
                    DataContext = new TestViewModel()
                };

                dialogFragment.Show(FragmentManager, "Episode02");
            };
            return view;
        }
    }
}