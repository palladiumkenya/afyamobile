using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;


namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ModuleRepository:BaseRepository<Module,Guid>,IModuleRepository
    {
        public ModuleRepository( string databasePath) : base( databasePath)
        {
        }

        public Module GetDefaultModule()
        {
            return GetAll().First();
        }

     
    }
}