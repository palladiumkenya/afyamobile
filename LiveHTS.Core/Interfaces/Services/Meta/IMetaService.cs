using System.Collections.Generic;
using LiveHTS.Core.Model.Meta;

namespace LiveHTS.Core.Interfaces.Services.Meta
{
    public interface IMetaService
    {
        IEnumerable<RegionItem> GetCounties(bool addSelectOption = true);
        IEnumerable<RegionItem> GetSubCounties(int countyId, bool addSelectOption = true);
        IEnumerable<RegionItem> GetWards(int subCountyId, bool addSelectOption = true);

    }
}