using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        string ErrorSummary { get; set; }
        
        string ResponseText { get; set; }
        bool ShowTextObs { get; set; }

        decimal ResponseNumeric { get; set; }
        bool ShowNumericObs { get; set; }

        ObservableCollection<CategoryItem> SingleOptions { get; set; } 
        CategoryItem SelectedSingleOption { get; set; }
        bool ShowSingleObs { get; set; }
        string OtherSingleResponseText { get; set; }
        bool ShowOtherSingleObs { get; set; }

        string Selection { get; set; }
        List<CategoryItem> MultiOptions { get; set; }
        List<CategoryItem> SelectedMultiOptions { get; set; }
        bool ShowMultiObs { get; set; }
        string OtherMultiResponseText { get; set; }
        bool ShowOtherMultiObs { get; set; }

        Concept Concept { get; set; }
        bool Allow { get; set; }
        
        void AllowNextQuestion();
        object GetResponse();
        void SetResponse(object response);
    }
}