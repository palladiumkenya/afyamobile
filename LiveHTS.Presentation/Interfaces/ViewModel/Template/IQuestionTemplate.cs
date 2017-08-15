using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IQuestionTemplate
    {
        Guid Id { get; set; }
        string Display { get; set; }
        Concept Concept { get; set; }
        string Response { get; set; }
        List<CategoryItem> SingleOptions { get; set; } 
        CategoryItem SelectedSingleOption { get; set; }

        List<CategoryItem> SingleOptionsList { get; set; }
        CategoryItem SelectedSingleOptionList { get; set; }

        List<CategoryItem> MultiOptions { get; set; }
        CategoryItem SelectedMultiOption { get; set; }
        

        bool ShowSingleObs { get; set; }
        bool ShowSingleObsList { get; set; }
        bool ShowTextObs { get; set; }
        bool ShowNumericObs { get; set; }
        bool ShowMultiObs { get; set; }

        string ShowSingleObsValue { get; }
        string ShowSingleObsListValue { get; }
        string ShowTextObsValue { get; }
        string ShowNumericObsValue { get; }
        string ShowMultiObsValue { get;  }
    }
}