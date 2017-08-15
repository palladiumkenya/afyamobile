using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class QuestionTemplate:IQuestionTemplate
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public Concept Concept { get; set; }

        
        public List<CategoryItem> SingleOptions { get; set; }=new List<CategoryItem>();
        public CategoryItem SelectedSingleOption { get; set; }

        public List<CategoryItem> SingleOptionsList { get; set; }=new List<CategoryItem>();
        public CategoryItem SelectedSingleOptionList { get; set; }

        public List<CategoryItem> MultiOptions { get; set; }=new List<CategoryItem>();
        public CategoryItem SelectedMultiOption { get; set; }


        public bool ShowSingleObs { get; set; }
        public bool ShowSingleObsList { get; set; }
        public bool ShowTextObs { get; set; }
        public bool ShowNumericObs { get; set; }
        public bool ShowMultiObs { get; set; }

        public QuestionTemplate(Question question)
        {
            Id = question.Id;
            Display = question.ToString();
            Concept = question.Concept;

            ShowTextObs = Concept.ConceptTypeId == "Text";
            ShowSingleObs = ShowSingleObsList= Concept.ConceptTypeId == "Single";
            ShowNumericObs = Concept.ConceptTypeId == "Numeric";
            ShowMultiObs = Concept.ConceptTypeId == "Multi";

            if (ShowSingleObs)
            {
                SingleOptions = SingleOptionsList = Concept.Category.Items;
            }
                
            if (ShowMultiObs)
                MultiOptions = Concept.Category.Items;

            MvxTrace.Error($"{Display}| Text:{ShowTextObs}, Single:{ShowSingleObs}");
        }
    }
}