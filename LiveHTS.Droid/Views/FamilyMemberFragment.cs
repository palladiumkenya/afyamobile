using Android.OS;
using Android.Views;
using Android.Widget;
using com.refractored.fab;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;

namespace LiveHTS.Droid.Views
{
    public class FamilyMemberFragment : MvxFragment<FamilyMemberViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.FamilyMemberView, null);

            var listView = view.FindViewById<ListView>(Resource.Id.listfamilymembers);
            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fabfamilymembers);
            fab.AttachToListView(listView);

            return view;
        }
    }
}