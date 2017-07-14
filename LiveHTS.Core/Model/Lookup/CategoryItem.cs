using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Lookup
{
    public class CategoryItem : Entity<Guid>
    {
        private string _display;

        [Indexed]
        public Guid CategoryId { get; set; }
        [Indexed]
        public Guid ItemId { get; set; }
        [Ignore]
        public Item Item { get; set; }

        public string Display
        {
            get { return string.IsNullOrWhiteSpace(_display) ? Item?.Display : _display; }
            set { _display = value; }
        }

        public Decimal Rank { get; set; }

        public CategoryItem()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return Display;
        }
    }
}