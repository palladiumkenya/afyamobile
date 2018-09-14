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

        public IEnumerable<RegionItem> GetCounties()
        {
            return _db.Query<RegionItem>($@"SELECT DISTINCT CountyId as Id,CountyName as Display FROM Region")
                .ToList()
                .OrderBy(x => x.Display);
        }

        public IEnumerable<RegionItem> GetSubCounties(int countyId)
        {
            return _db.Query<RegionItem>($@"SELECT DISTINCT SubcountyId as Id,Subcountyname as Display FROM Region WHERE CountyId=?",countyId)
                .ToList()
                .OrderBy(x => x.Display);
        }

        public IEnumerable<RegionItem> GetWards(int subCountyId)
        {
            return _db.Query<RegionItem>($@"SELECT DISTINCT WardId as Id,WardName as Display FROM Region WHERE SubcountyId=?", subCountyId)
                .ToList()
                .OrderBy(x => x.Display);
        }

        public RegionItem GetCounty(int countyId)
        {
            return _db.Query<RegionItem>($@"SELECT CountyId as Id,CountyName as Display FROM Region WHERE CountyId=? LIMIT 1", countyId)
                .ToList()
                .FirstOrDefault();
        }

        public RegionItem GetSubCounty(int subCountyId)
        {
            return _db.Query<RegionItem>($@"SELECT SubcountyId as Id,Subcountyname as Display FROM Region WHERE SubcountyId=?", subCountyId)
                .ToList()
                .FirstOrDefault();
        }

        public RegionItem GetWard(int wardId)
        {
            return _db.Query<RegionItem>($@"SELECT WardId as Id,WardName as Display FROM Region WHERE WardId=?", wardId)
                .ToList()
                .FirstOrDefault();
        }
    }
}