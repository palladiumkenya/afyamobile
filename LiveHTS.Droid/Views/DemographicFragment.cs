using Android.OS;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.FullFragging.Fragments;

namespace LiveHTS.Droid.Views
{
    public class DemographicFragment : MvxFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.ClientDemographicView, null);
        }
    }
}