using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Module:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Form> Forms { get; set; }=new List<Form>();
    }
}