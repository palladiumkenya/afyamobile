using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model
{
    public class Practice:Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PracticeTypeId { get; set; }

        public Practice(string code, string name, string description, Guid practiceTypeId)
        {
            Code = code;
            Name = name;
            Description = description;
            PracticeTypeId = practiceTypeId;
        }
    }
}