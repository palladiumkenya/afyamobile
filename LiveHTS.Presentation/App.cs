using System.Reflection;
using LiveHTS.Core.Model;
using LiveHTS.Infrastructure.Repository;
using LiveHTS.Infrastructure.Repository.Survey;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace LiveHTS.Presentation
{
    public class App:MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            var assemblyCore = typeof(Module).GetTypeInfo().Assembly;
            var assemblyInfrastructure = typeof(ModuleRepository).GetTypeInfo().Assembly;

            CreatableTypes(assemblyCore)
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes(assemblyInfrastructure)
                .EndingWith("Repository")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes(assemblyInfrastructure)
                .EndingWith("LiveDatabase")
                .AsInterfaces()
                .RegisterAsSingleton();

            RegisterAppStart(new AppStart());
        }
    }
}