﻿using Android.OS;
using Android.Runtime;
using Android.Views;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.FullFragging.Fragments;
using MvvmCross.Droid.Shared.Attributes;


namespace LiveHTS.Droid.Views
{
    [MvxFragment(typeof(ClientRegistrationViewModel), Resource.Id.content_frame, true)]
    [Register("livehts.droid.views.ClientContactFragment")]
    public class ClientContactFragment : MvxFragment<ClientContactViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored=base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.ClientContactView, null);
        }
    }
}