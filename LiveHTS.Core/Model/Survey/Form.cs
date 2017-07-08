using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Form:Entity<Guid>
    {
        public string Name { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public decimal Rank { get; set; }
        public Guid ModuleId { get; set; }
        public List<Question> Questions { get; set; }=new List<Question>();

        public void AddQuestion(Question question)
        {
            question.FormId = Id;
            Questions.Add(question);
        }
        public void AddQuestion(List<Question> questions)
        {
            foreach (var question in questions)
            {
                AddQuestion(question);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}