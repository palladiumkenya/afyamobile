using System;
using LiveHTS.Core.Model.Lookup;

namespace LiveHTS.Core.Event
{
    public class OptionSelectedEventArgs
    {
        public CategoryItem CategoryItem { get; }

        public OptionSelectedEventArgs(Guid id, bool isSelected)
        {
            CategoryItem=CategoryItem.CreateForNotification(id,isSelected);
        }
    }
}