using System;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Lookup
{
    public class CategoryItem : Entity<Guid>
    {
        [Indexed]
        public Guid CategoryId { get; set; }
        [Indexed]
        public Guid ItemId { get; set; }
        public string Display { get; set; }
        public Decimal Rank { get; set; }
    }
}