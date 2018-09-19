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

        public int GetIntValue(int defaultVal = 1)
        {
            int.TryParse(Value, out var val);
            return val == 0 ? defaultVal : val;
        }

        public override string ToString()
        {
            return Display;
        }
    }
}