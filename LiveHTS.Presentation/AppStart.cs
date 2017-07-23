using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Infrastructure.Migrations;
using LiveHTS.Presentation.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace LiveHTS.Presentation
{
    public class AppStart: MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            var migrator=Mvx.Resolve<IMigrator>();
            migrator.Migrate();

            ShowViewModel<SignInViewModel>();
        }
    }
}