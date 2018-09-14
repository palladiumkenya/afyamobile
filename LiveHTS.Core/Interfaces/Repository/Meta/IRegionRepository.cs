using System.Collections.Generic;
using LiveHTS.Core.Model.Meta;

namespace LiveHTS.Core.Interfaces.Repository.Meta
{
    public interface IRegionRepository : IMetaRepository<Region,int>
    {
        IEnumerable<RegionItem> GetCounties();
        IEnumerable<RegionItem> GetSubCounties(int countyId);
        IEnumerable<RegionItem> GetWards(int subCountyId);

        RegionItem GetCounty(int countyId);
        RegionItem GetSubCounty(int subCountyId);
        RegionItem GetWard(int wardId);
    }
}