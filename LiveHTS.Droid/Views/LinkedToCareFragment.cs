using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using LiveHTS.Droid.Custom;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Shared.Attributes;


namespace LiveHTS.Droid.Views
{
//    [MvxFragment(typeof(HIVTestViewModel), Resource.Id.content_frame)]
//    [Register("livehts.droid.views.FirstHIVTestFragment")]
    public class LinkedToCareFragment : MvxFragment<LinkedToCareViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            ViewModel.ChangedEnrollDate += ViewModel_ChangedEnrollDate;
            ViewModel.ChangedArtDate += ViewModel_ChangedArtDate;

            return this.BindingInflate(Resource.Layout.LinkedToCareView, null);
        }

        private void ViewModel_ChangedEnrollDate(object sender, Presentation.Events.ChangedDateEvent e)
        {
            DatePickerFragmentV4 frag = DatePickerFragmentV4.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedEnrolDate = new TraceDateDTO(e.Id, time.Date);
            }, e.Date);

            frag.Show(FragmentManager, DatePickerFragmentV4.TAG);
        }

        private void ViewModel_ChangedArtDate(object sender, Presentation.Events.ChangedDateEvent e)
        {
            DatePickerFragmentV4 frag = DatePickerFragmentV4.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedArtDate = new TraceDateDTO(e.Id, time.Date);
            }, e.Date);

            frag.Show(FragmentManager, DatePickerFragmentV4.TAG);
        }
    }
}
