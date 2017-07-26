using Android.OS;
using Android.Runtime;
using Android.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace LiveHTS.Droid.Views
{
    [MvxFragment(typeof(ClientRegistrationViewModel), Resource.Id.content_frame, true)]
    [Register("livehts.droid.views.ClientDemographicFragment")]
    public class ClientDemographicFragment : MvxFragment<ClientDemographicViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored=base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.ClientDemographicView, null);
        }
    }
}