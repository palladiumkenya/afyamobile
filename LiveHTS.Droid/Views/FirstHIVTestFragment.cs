using Android.OS;
using Android.Runtime;
using Android.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Shared.Attributes;


namespace LiveHTS.Droid.Views
{
//    [MvxFragment(typeof(HIVTestViewModel), Resource.Id.content_frame)]
//    [Register("livehts.droid.views.FirstHIVTestFragment")]
    public class FirstHIVTestFragment : MvxFragment<FirstHIVTestViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.FirstHIVTestView, null);

//            var vm = new PersonViewModel();
//
//            ViewModel.ShowTaskCommandAction = () => {
//                var dialogFragment = new PersonFragment()
//                {
//                    DataContext = vm
//                };
//
//                dialogFragment.Show(SupportFragmentManager, "Person");
//            };
        }
    }
}