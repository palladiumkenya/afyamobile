using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Infrastructure.Repository;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace LiveHTS.Presentation
{
    public class AppStart: MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            var migrator= Mvx.Resolve<IDbMigrator>();
            migrator.Migrate();
            ShowViewModel<SignInViewModel>();
        }
    }
}