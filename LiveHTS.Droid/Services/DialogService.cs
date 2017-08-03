using Android.App;
using LiveHTS.Presentation.Interfaces;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace LiveHTS.Droid.Services
{
    public class DialogService:IDialogService
    {
        public void Alert(string message, string title, string okbtnText)
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);
            adb.SetIcon(Resource.Drawable.Icon);
            adb.SetPositiveButton(okbtnText, (sender, args) => { /* some logic */ });
            adb.Create().Show();
        }
        public void ConfirmExit()
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle("Exit");
            adb.SetMessage("Are you sure you want to Quit ?");
            adb.SetIcon(Resource.Drawable.Icon);
            adb.SetPositiveButton("Yes", (sender, args) => {act.FinishAffinity();});
            adb.SetNegativeButton("No", (sender, args) => { /* some logic */ });
            adb.Create().Show();
        }
    }
}