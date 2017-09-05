using System;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Config
{
    public class Device:Entity<Guid>
    {
        public string Serial { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Indexed]
        public Guid PracticeId { get; set; }

        public Device()
        {
        }

        public Device(string serial, string code, string name, Guid practiceId)
        {
            Serial = serial;
            Code = code;
            Name = name;
            PracticeId = practiceId;
        }

        public override string ToString()
        {
            return $"{Name} ({Serial})";
        }
    }
}