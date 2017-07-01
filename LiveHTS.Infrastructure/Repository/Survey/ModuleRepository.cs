using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.DummyData;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ModuleRepository : BaseRepository<Module>,IModuleRepository
    {
        public ModuleRepository()
        {
            _entities = LiveDatabase.Read().ToList();
        }

        public Module GetDefaultModule()
        {
           return _entities.First();
        }
    }
}