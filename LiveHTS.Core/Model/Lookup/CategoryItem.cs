using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Lookup
{
    public class CategoryItem : Entity<Guid>
    {
        public Guid CategoryId { get; set; }
        public Guid ItemId { get; set; }
        public string Display { get; set; }
        public Decimal Rank { get; set; }
    }
}