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

        public override string ToString()
        {
            return Display;
        }
    }
}