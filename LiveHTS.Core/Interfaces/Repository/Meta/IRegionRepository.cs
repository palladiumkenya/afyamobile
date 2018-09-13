using System.Collections.Generic;
using LiveHTS.Core.Model.Meta;

namespace LiveHTS.Core.Interfaces.Repository.Meta
{
    public interface IRegionRepository : IMetaRepository<Region,int>
    {
        IEnumerable<Region> GetCounties();
        IEnumerable<Region> GetSubCounties(int countyId);
        IEnumerable<Region> GetWards(int subCountyId);

        Region GetCounty(int countyId);
        Region GetSubCounty(int subCountyId);
        Region GetWard(int wardId);
    }
}