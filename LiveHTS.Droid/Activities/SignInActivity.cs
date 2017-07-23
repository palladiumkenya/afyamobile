using Android.App;
using Android.Content.PM;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Droid.Views;

namespace LiveHTS.Droid.Activities
{
    [Activity(Label = "Sign In",LaunchMode = LaunchMode.SingleTop,
        Theme = "@android:style/Theme.Material.Light")]
    public class SignInActivity: MvxActivity<SignInViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.SignInView);
        }
    }
}