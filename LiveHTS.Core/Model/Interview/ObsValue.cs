using System;
using System.Runtime.InteropServices.ComTypes;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsValue
    {
        public Type Type { get; set; }
        public object Value { get; set; }

        public ObsValue(Type type,object value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value} [{Type}]";
        }
    }
}