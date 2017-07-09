using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;


namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ModuleRepository : BaseRepository<Module, Guid>, IModuleRepository
    {
        public ModuleRepository(string databasePath) : base(databasePath)
        {
        }

        public Module GetDefaultModule()
        {
            return GetAll().First();
        }

        public override IEnumerable<Module> GetAll()
        {
            var modules = base.GetAll().ToList();

//            foreach (var module in modules)
//            {
//                try
//                {
//                    var forms = _db.Table<Form>().Where(x => x.ModuleId == module.Id).ToList();
//                    if (forms.Count > 0)
//                        module.Forms = forms;
//                }
//                catch
//                {
//                    // ignored
//                }
//            }

            return modules;
        }
    }
}