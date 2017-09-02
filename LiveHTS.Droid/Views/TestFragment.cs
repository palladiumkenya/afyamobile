using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using LiveHTS.Droid.Custom;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using Fragment = Android.Support.V4.App.Fragment;


namespace LiveHTS.Droid.Views
{
    [Register("livehts.droid.views.TestFragment")]
    public class TestFragment : MvxDialogFragment<TestViewModel>
    {
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            EnsureBindingContextSet(savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.TestView, null);
            var dialog = new AlertDialog.Builder(Activity);
            dialog.SetView(view);
            return dialog.Create();
        }
    }
}