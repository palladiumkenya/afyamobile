using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Meta;
using LiveHTS.Core.Model.Meta;

namespace LiveHTS.Infrastructure.Repository.Meta
{
    public class KitHistoryRepository : MetaRepository<KitHistory, Guid>, IKitHistoryRepository
    {
        public KitHistoryRepository(ILiveSetting liveSetting) : base(liveSetting)
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
    }
}