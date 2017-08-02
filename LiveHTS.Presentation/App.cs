using System.Reflection;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Repository.Survey;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace LiveHTS.Presentation
{
    public class App:MvxApplication
    {
        private readonly string _dbpath;
    
        public App(string dbpath)
        {
            _dbpath = dbpath;
        }

        public override void Initialize()
        {
            base.Initialize();

            var assemblyCore = typeof(Concept).GetTypeInfo().Assembly;
            var assemblyInfrastructure = typeof(ModuleRepository).GetTypeInfo().Assembly;

            CreatableTypes(assemblyCore)
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes(assemblyInfrastructure)
                .EndingWith("Repository")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<ILiveSetting>(new LiveSetting(_dbpath));
            
            CreatableTypes(assemblyInfrastructure)
                .EndingWith("Migrator")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart(new AppStart());

        }
    }
}