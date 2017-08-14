using System;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class QuestionTemplate:IQuestionTemplate
    {
        public Guid Id { get; set; }
        public string Display { get; set; }

        public QuestionTemplate(Question question)
        {
            Id = question.Id;
            Display = question.Display;
        }
    }
}