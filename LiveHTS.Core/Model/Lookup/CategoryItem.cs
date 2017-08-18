using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveHTS.Core.Annotations;
using LiveHTS.Core.Event;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Lookup
{
    public class CategoryItem : Entity<Guid>
    {
        private string _display;
        private bool _selected;
        private bool _allow = true;

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

        [Ignore]
        public bool Selected
        {
            get { return _selected; }
            set
            {

                if (value != _selected)
                {
                    _selected = value;
                    NotifyOptionChanged(Id, _selected);
                }
            }
        }

        [Ignore]
        public bool Allow
        {
            get { return _allow; }
            set { _allow = value; }
        }

        public event EventHandler<OptionSelectedEventArgs> OptionSelected;

        public CategoryItem()
        {
            Id = LiveGuid.NewGuid();
        }

        private CategoryItem(Guid id, bool selected) 
        {
            Id = id;
            Selected = selected;
        }

        private CategoryItem(Guid id, Guid categoryId, Guid itemId, string display, decimal rank)
        {
            Id = id;
            _display = display;
            CategoryId = categoryId;
            ItemId = itemId;
            Rank = rank;
        }

        public static CategoryItem CreateInitial(string display)
        {
            return new CategoryItem(Guid.Empty, Guid.Empty, Guid.Empty,display,-1);
        }

        public static CategoryItem CreateForNotification(Guid id, bool selected)
        {
            return new CategoryItem(id,selected);
        }

        private void NotifyOptionChanged(Guid id,bool isSelected)
        {
            if (OptionSelected != null)
            {
                OptionSelected(this, new OptionSelectedEventArgs(id,isSelected));
            }
        }

        public override string ToString()
        {
            return Display;
        }
    }
}