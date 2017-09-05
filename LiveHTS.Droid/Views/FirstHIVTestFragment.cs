using Android.OS;
using Android.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;


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
            var view =this.BindingInflate(Resource.Layout.FirstHIVTestView, null);
            
            ViewModel.AddTestCommandAction = () => {
                var dialogFragment = new TestFragment()
                {
                    DataContext = new TestViewModel()
                };

                dialogFragment.Show(FragmentManager, "TEST");
            };
            return view;
        }
    }
}