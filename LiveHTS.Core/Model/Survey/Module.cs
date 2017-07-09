using System;
using System.Collections.Generic;
using System.Linq;
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
      
        //public List<Form> Forms { get; set; }=new List<Form>();

        public Module()
        {
            Id = LiveGuid.NewGuid();
        }
        /*
        public void AddForm(Form form)
        {
            form.ModuleId = Id;
            Forms.Add(form);
        }
        public void AddForms(List<Form> forms)
        {
            foreach (var form in forms)
            {
                AddForm(form);
            }
        }
        */

        public override string ToString()
        {
            return $"{Name},{Display}";
        }
    }
}