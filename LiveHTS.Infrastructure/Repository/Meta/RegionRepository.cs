using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Meta;
using LiveHTS.Core.Model.Meta;

namespace LiveHTS.Infrastructure.Repository.Meta
{
    public class RegionRepository : MetaRepository<Region, int>, IRegionRepository
    {
        public RegionRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
            /*
             
             return _db.Query<Region>($@"
            SELECT DISTINCT
                CountyId,
                CountyName,
                SubcountyId,
                Subcountyname,
                WardId,
                WardName
            FROM
                Region
            WHERE 
                Symbol = ?", "A");

             
             */
        }

        public IEnumerable<Region> GetCounties()
        {
            return _db.Query<Region>($@"SELECT DISTINCT CountyId,CountyName FROM Region")
                .ToList()
                .OrderBy(x => x.CountyName);
        }

        public IEnumerable<Region> GetSubCounties(int countyId)
        {
            return _db.Query<Region>($@"SELECT DISTINCT SubcountyId,Subcountyname FROM Region WHERE CountyId=?",countyId)
                .ToList()
                .OrderBy(x => x.SubCountyName);
        }

        public IEnumerable<Region> GetWards(int subCountyId)
        {
            return _db.Query<Region>($@"SELECT DISTINCT WardId,WardName FROM Region WHERE SubcountyId=?", subCountyId)
                .ToList()
                .OrderBy(x => x.WardName);
        }

        public Region GetCounty(int countyId)
        {
            return _db.Query<Region>($@"SELECT CountyId,CountyName FROM Region WHERE CountyId=? LIMIT 1", countyId)
                .ToList()
                .FirstOrDefault();
        }

        public Region GetSubCounty(int subCountyId)
        {
            return _db.Query<Region>($@"SELECT SubcountyId,Subcountyname FROM Region WHERE SubcountyId=?", subCountyId)
                .ToList()
                .FirstOrDefault();
        }

        public Region GetWard(int wardId)
        {
            return _db.Query<Region>($@"SELECT WardId,WardName FROM Region WHERE WardId=?", wardId)
                .ToList()
                .FirstOrDefault();
        }
    }
}