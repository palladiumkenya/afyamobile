using System;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using SQLite;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ModuleRepository:BaseRepository<Module,Guid>,IModuleRepository
    {
        public ModuleRepository()
        {
        }

        public ModuleRepository(SQLiteConnection db) : base(db)
        {
        }

        public Module GetDefaultModule()
        {
            throw new NotImplementedException();
        }
    }
}