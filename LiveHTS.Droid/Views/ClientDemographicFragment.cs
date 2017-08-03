using Android.OS;
using Android.Runtime;
using Android.Views;
using LiveHTS.Droid.Activities;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.FullFragging.Fragments;
using MvvmCross.Droid.Shared.Attributes;


namespace LiveHTS.Droid.Views
{
    [MvxFragment(typeof(ClientRegistrationViewModel), Resource.Id.content_frame)]
    [Register("livehts.droid.views.ClientDemographicFragment")]
    public class ClientDemographicFragment : MvxFragment<ClientDemographicViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var ignored=base.OnCreateView(inflater, container, savedInstanceState);
            var view= this.BindingInflate(Resource.Layout.ClientDemographicView,container,false);            
            return view;           
        }  
        
    }
}