using System;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsValue
    {
        public Type Type { get; set; }
        public object Value { get; set; }

        public ObsValue(object value)
        {
            Type = value.GetType();
            Value = value;
        }
    }
}