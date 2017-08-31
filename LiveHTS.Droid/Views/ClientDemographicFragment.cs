using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using LiveHTS.Droid.Activities;
using LiveHTS.Droid.Custom;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;

using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;


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
            ViewModel.ChangedDate += ViewModel_ChangedDate;
            return view;           
        }

        private void ViewModel_ChangedDate(object sender, Presentation.Events.ChangedDateEvent e)
        {
            DatePickerFragmentV4 frag = DatePickerFragmentV4.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedDate = new TraceDateDTO(e.Id, time.Date);
            }, e.Date);

            frag.Show(FragmentManager, DatePickerFragmentV4.TAG);
        }
    }
}