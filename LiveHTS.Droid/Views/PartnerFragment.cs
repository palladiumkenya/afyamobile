using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using com.refractored.fab;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Shared.Attributes;


namespace LiveHTS.Droid.Views
{
    public class PartnerFragment : MvxFragment<PartnerViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view= this.BindingInflate(Resource.Layout.PartnerView, null);

            var listView = view.FindViewById<ListView>(Resource.Id.listpartners);
            var fab =view.FindViewById<FloatingActionButton>(Resource.Id.fabpartners);
            fab.AttachToListView(listView);

            return view;
        }
    }
}