using Android.OS;
using Android.Runtime;
using Android.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;

using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;


namespace LiveHTS.Droid.Views
{
    [MvxFragment(typeof(ClientRegistrationViewModel), Resource.Id.content_frame)]
    [Register("livehts.droid.views.ClientEnrollmentFragment")]
    public class ClientEnrollmentFragment : MvxFragment<ClientEnrollmentViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var ignored=base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.ClientEnrollmentView, null);
            var id = this.Id;
        }
    }
}