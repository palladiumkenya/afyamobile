using System.Windows.Input;
using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ISignInViewModel
    {
        User User { get; }
        string Username { get; set; }
        string Password { get; set; }
        IMvxCommand SignInCommand { get; }
        bool IsBusy { get; set; }
    }
}