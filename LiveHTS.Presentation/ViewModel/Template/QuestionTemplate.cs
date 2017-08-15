using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class QuestionTemplate:IQuestionTemplate
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public Concept Concept { get; set; }

        public bool ShowSingleObs { get; set; }
        public List<CategoryItem> SingleOptions { get; set; }=new List<CategoryItem>();
        public CategoryItem SelectedOption { get; set; }

        public bool ShowTextObs { get; set; }

        public QuestionTemplate(Question question)
        {
            Id = question.Id;
            Display = question.ToString();
            Concept = question.Concept;
            ShowTextObs = Concept.ConceptTypeId == "Text";
            ShowSingleObs = Concept.ConceptTypeId == "Single";

            if (ShowSingleObs)
                SingleOptions = Concept.Category.Items;
        }
    }
}