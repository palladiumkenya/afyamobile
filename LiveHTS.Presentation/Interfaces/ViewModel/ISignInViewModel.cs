using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ISignInViewModel
    {
        User User { get; }
        string Facility { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        bool IsBusy { get; set; }
        bool AutoSignIn { get; set; }
        IMvxCommand SignInCommand { get; }
        IMvxCommand SetUpCommand { get; }
        void LoadDeviceInfo(string serial, string model, string manufacturer);
        void UpdateSession();
    }
}