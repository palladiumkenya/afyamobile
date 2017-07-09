using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Module:Entity<Guid>
    {
        private Guid _id;
        
        public override Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

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
    }
}