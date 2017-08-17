using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IQuestionTemplate
    {
        IQuestionTemplateWrap QuestionTemplateWrap { get; set; }

        Guid Id { get; set; }
        string Display { get; set; }
        Concept Concept { get; set; }
        bool Allow { get; set; }
        string ResponseText { get; set; }
        decimal ResponseNumeric { get; set; }
        string ErrorSummary { get; set; }
        List<CategoryItem> SingleOptions { get; set; } 
        CategoryItem SelectedSingleOption { get; set; }

        List<CategoryItem> SingleOptionsList { get; set; }
        CategoryItem SelectedSingleOptionList { get; set; }

        string Selection { get; set; }
        List<CategoryItem> MultiOptions { get; set; }
        List<CategoryItem> SelectedMultiOptions { get; set; }
        
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

        void MoveToQuestionNext();
        object GetResponse();
        void SetResponse(object response);
        IMvxCommand CheckOptionCommand { get; }
    }
}