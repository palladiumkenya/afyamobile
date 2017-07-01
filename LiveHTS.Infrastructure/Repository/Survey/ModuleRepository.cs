using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.DummyData;
using ILiveDatabase = LiveHTS.Core.Interfaces.Repository.ILiveDatabase;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ModuleRepository : BaseRepository<Module>,IModuleRepository
    {
        public ModuleRepository(ILiveDatabase database) : base(database)
        {
            _entities = database.Read();
        }
        public Module GetDefaultModule()
        {
           return _entities.First();
        }
    }
}