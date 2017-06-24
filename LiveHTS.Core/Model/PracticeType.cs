using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model
{
    public class PracticeType:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Practice> Practices { get; set; }=new List<Practice>();
        public PracticeType(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}