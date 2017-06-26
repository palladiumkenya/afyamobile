using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Section:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Concept> Concepts { get; set; }=new List<Concept>();
        public Guid FormId { get; set; }
    }
}