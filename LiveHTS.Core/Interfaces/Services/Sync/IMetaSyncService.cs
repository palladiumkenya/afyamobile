using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.SyncModel;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IMetaSyncService
    {
        Task<SyncModel.Meta> GetMetaData(string url);
        Task<List<County>> GetCounties(string url);
        Task<List<Category>> GetCategories(string url);
        Task<List<Item>> GetItems(string url);
        Task<List<CategoryItem>> GetCatItems(string url);
      
    }
}