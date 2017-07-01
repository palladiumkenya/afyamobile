using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Form:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Section> Sections { get; set; }=new List<Section>();
        public Guid ModuleId { get; set; }

        public void AddSection(Section section)
        {
            section.FormId = Id;
            Sections.Add(section);
        }
        public void AddSections(List<Section> sections)
        {
            foreach (var section in sections)
            {
                AddSection(section);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}