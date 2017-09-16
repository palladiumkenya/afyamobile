using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.SyncModel;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IFormsSyncService
    {
        Task<List<Module>> GetModules(string url);
        Task<List<Form>> GetForms(string url);
        Task<List<Concept>> GetConcepts(string url);
        Task<List<Question>> GetQuestions(string url);
      
    }
}