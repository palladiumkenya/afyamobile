using LiveHTS.Core.Interfaces.Repository;
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

            
            //ShowViewModel<SignInViewModel>();
            
            ShowViewModel<DashboardViewModel>(new
            {
                id = "4547b7e0-98c7-4c6f-9d2a-a7b7016df232",
            });
            /*
            ShowViewModel<HIVTestViewModel>(new
                        {
                            encounterTypeId= "b262f4ee-852f-11e7-bb31-be2e44b06b34",
                            mode = "new",
                            clientId= "4547b7e0-98c7-4c6f-9d2a-a7b7016df232",
                            encounterId = ""
                        });
                        */
            //string encounterTypeId, string mode, string clientId, string encounterId
        }
    }
}