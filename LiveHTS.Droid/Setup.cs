using System.IO;
using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Views;
using MvvmCross.Platform.Platform;
using MvvmCross.Droid.Shared.Presenter;
using MvvmCross.Platform;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Droid.Services;

using System.Reflection;
using LiveHTS.Presentation.Converters;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Binding.Bindings.Target.Construction;
using LiveHTS.Droid.Custom;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace LiveHTS.Droid
{
    public class Setup : MvxAndroidSetup
    {
        private IDialogService _dialogService;
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            var dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "livehts.db"); //Create New Database

            return new Presentation.App(dbpath);
        }

//        protected override IMvxTrace CreateDebugTrace()
//        {
//            return new DebugTrace();
//        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.RegisterSingleton<IDialogService>(() => new DialogService());

        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies
        {
            get
            {
                var assemblies = base.AndroidViewAssemblies.ToList();
                assemblies.Add(typeof(com.refractored.fab.FloatingActionButton).Assembly);
                return assemblies;
            }
        }

        protected override IEnumerable<Assembly> ValueConverterAssemblies
        {
            get
            {
                var toReturn = base.ValueConverterAssemblies.ToList();
                toReturn.Add(typeof(DmyValueConverter).Assembly);
                toReturn.Add(typeof(ShowControlConverter).Assembly);
                toReturn.Add(typeof(ShowRequiredConverter).Assembly);
                toReturn.Add(typeof(ClientTextColorConverter).Assembly);
                return toReturn;
            }
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxFragmentsPresenter(AndroidViewAssemblies);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);
            return mvxFragmentsPresenter;
        }
    }
}
