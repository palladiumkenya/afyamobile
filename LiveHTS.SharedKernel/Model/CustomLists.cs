using System.Collections.Generic;

namespace LiveHTS.SharedKernel.Model
{
    public class CustomItem
    {
        public string Display { get; set; }
        public string Value { get; set; }

        public CustomItem(string value)
        {
            Value=Display = value;
        }

        public CustomItem(string value,string display )
        {
            Display = display;
            Value = value;
        }
    }
    public class CustomLists
    {
        public static List<CustomItem> Gender = new List<CustomItem> {new CustomItem("M"), new CustomItem("F")};
    }
}