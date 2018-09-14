using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;

using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;


namespace LiveHTS.Droid.Views
{
    [MvxFragment(typeof(ClientRegistrationViewModel), Resource.Id.content_frame)]
    [Register("livehts.droid.views.ClientContactFragment")]
    public class ClientContactFragment : MvxFragment<ClientContactViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var ignored=base.OnCreateView(inflater, container, savedInstanceState);

            
            var view= this.BindingInflate(Resource.Layout.ClientContactView, null);

            var spinnerCounty = view.FindViewById<Spinner>(Resource.Id.spinnerCounties);
            spinnerCounty.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(County_Selected);

            var spinnerSubCounty = view.FindViewById<Spinner>(Resource.Id.spinnerSubCounties);
            spinnerSubCounty.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SubCounty_Selected);

            return view;
        }

        private void County_Selected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ViewModel.LoadSubCounties(e.Position);
        }

        private void SubCounty_Selected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ViewModel.LoadSubWards(e.Position);
        }
    }
}