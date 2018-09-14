using System.Collections.Generic;

namespace LiveHTS.Core.Model.Meta
{
    public class RegionItem 
    {
        private int _id;
        private string _display;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Display
        {
            get { return _display; }
            set { _display = value; }
        }

        public static List<RegionItem> Init(string initial)
        {
            return new List<RegionItem>()
            {
                CreateCountyInitial($"Select {initial}")
            };
        }

        public static RegionItem CreateCountyInitial(string display = "Select County")
        {
            return new RegionItem
            {
                Id = 0,
                Display = display
            };
        }

        public static RegionItem CreateSubCountyInitial(string display = "Select SubCounty")
        {
            return new RegionItem
            {
                Id = 0,
                Display = display
            };
        }

        public static RegionItem CreateWardInitial(string display = "Select Ward")
        {
            return new RegionItem
            {
                Id = 0,
                Display = display
            };
        }
        public override string ToString()
        {
            return Display;
        }
    }
}