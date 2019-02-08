using System;
using System.Collections.Generic;

namespace LiveHTS.SharedKernel.Model
{
    public class CustomItem : IEquatable<CustomItem>
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

        public int GetIntValue(int defaultVal = 1)
        {
            int.TryParse(Value, out var val);
            return val == 0 ? defaultVal : val;
        }

        public override string ToString()
        {
            return Display;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CustomItem);
        }

        public bool Equals(CustomItem other)
        {
            return other != null &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }

        public static bool operator ==(CustomItem item1, CustomItem item2)
        {
            return EqualityComparer<CustomItem>.Default.Equals(item1, item2);
        }

        public static bool operator !=(CustomItem item1, CustomItem item2)
        {
            return !(item1 == item2);
        }
    }
}