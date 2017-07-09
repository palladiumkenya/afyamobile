using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Module:Entity<Guid>
    {
        public string Name { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public decimal Rank { get; set; }
      
        //[Ignore]
        //public List<Form> Forms { get; set; }=new List<Form>();

        public Module()
        {
            Id = LiveGuid.NewGuid();
        }
        public override string ToString()
        {
            return $"{Display}";
        }
    }
}