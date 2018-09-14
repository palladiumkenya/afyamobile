using System.Collections.Generic;
using LiveHTS.Core.Model.Meta;

namespace LiveHTS.Core.Interfaces.Services.Meta
{
    public interface IMetaService
    {
        IEnumerable<Region> GetCounties(bool addSelectOption = true);
        IEnumerable<Region> GetSubCounties(int countyId, bool addSelectOption = true);
        IEnumerable<Region> GetWards(int subCountyId, bool addSelectOption = true);

    }
}