using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using LiveHTS.Droid.Custom;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;


namespace LiveHTS.Droid.Views
{
    [Register("livehts.droid.views.TestFragment")]
    public class TestFragment : MvxDialogFragment<TestViewModel>
    {
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            EnsureBindingContextSet(savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.TestView, null);

            ViewModel.ChangedDate += ViewModel_ChangedDate;
            var dialog = new AlertDialog.Builder(Activity);
            dialog.SetView(view);
            return dialog.Create();
        }

        private void ViewModel_ChangedDate(object sender, ChangedDateEvent e)
        {
            DatePickerFragmentV4 frag = DatePickerFragmentV4.NewInstance(
                delegate (DateTime time)
            {
                ViewModel.SelectedDate = new TraceDateDTO(e.Id, time.Date);
            }, e.Date,true,false);

            frag.Show(FragmentManager, DatePickerFragmentV4.TAG); 
        }
    }
}