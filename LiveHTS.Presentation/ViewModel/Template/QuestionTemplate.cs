using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class QuestionTemplate : MvxNotifyPropertyChanged, IQuestionTemplate
    {
        private Guid _id;
        private string _display;
        private Concept _concept;
        private string _responseText;
        private List<CategoryItem> _singleOptions = new List<CategoryItem>();
        private CategoryItem _selectedSingleOption;
        private List<CategoryItem> _singleOptionsList = new List<CategoryItem>();
        private CategoryItem _selectedSingleOptionList;
        private List<CategoryItem> _multiOptions = new List<CategoryItem>();
        private CategoryItem _selectedMultiOption;
        private bool _showSingleObs;
        private bool _showSingleObsList;
        private bool _showTextObs;
        private bool _showNumericObs;
        private bool _showMultiObs;
        private decimal _responseNumeric;
        private bool _allow;
        private string _errorSummary;


        public IQuestionTemplateWrap QuestionTemplateWrap { get; set; }

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        public string Display
        {
            get { return _display; }
            set
            {
                _display = value;
                RaisePropertyChanged(() => Display);
            }
        }

        public Concept Concept
        {
            get { return _concept; }
            set
            {
                _concept = value;
                RaisePropertyChanged(() => Concept);
            }
        }

        public bool Allow
        {
            get { return _allow; }
            set
            {
                _allow = value; RaisePropertyChanged(() => Allow);
                if (ShowSingleObs)
                {
                    foreach (var categoryItem in SingleOptions)
                    {
                        categoryItem.Allow = true;
                    }
                }
//                if (ShowMultiObs)
//                {
//                    foreach (var categoryItem in MultiOptions)
//                    {
//                        categoryItem.Allow = true;
//                    }
//                }
            }
        }

        public string ResponseText
        {
            get { return _responseText; }
            set
            {
                _responseText = value;
               RaisePropertyChanged(() => ResponseText);
                MoveToQuestionNext();
            }
        }

        public decimal ResponseNumeric
        {
            get { return _responseNumeric; }
            set { _responseNumeric = value; RaisePropertyChanged(() => ResponseNumeric); MoveToQuestionNext(); }
        }

        public string ErrorSummary
        {
            get { return _errorSummary; }
            set { _errorSummary = value; RaisePropertyChanged(() => ErrorSummary);}
        }

        public List<CategoryItem> SingleOptions
        {
            get { return _singleOptions; }
            set
            {
                _singleOptions = value;
                RaisePropertyChanged(() => SingleOptions);
            }
        }

        public CategoryItem SelectedSingleOption
        {
            get { return _selectedSingleOption; }
            set
            {
                _selectedSingleOption = value;
                RaisePropertyChanged(() => SelectedSingleOption);
                MoveToQuestionNext();
            }
        }

        public List<CategoryItem> SingleOptionsList
        {
            get { return _singleOptionsList; }
            set
            {
                _singleOptionsList = value;
                RaisePropertyChanged(() => SingleOptionsList);
            }
        }

        public CategoryItem SelectedSingleOptionList
        {
            get { return _selectedSingleOptionList; }
            set
            {
                _selectedSingleOptionList = value;
                RaisePropertyChanged(() => SelectedSingleOptionList);
                MoveToQuestionNext();
            }
        }

        public List<CategoryItem> MultiOptions
        {
            get { return _multiOptions; }
            set
            {
                _multiOptions = value;
                RaisePropertyChanged(() => MultiOptions);                
            }
        }

        public CategoryItem SelectedMultiOption
        {
            get { return _selectedMultiOption; }
            set
            {
                _selectedMultiOption = value;
                RaisePropertyChanged(() => SelectedMultiOption);
                MoveToQuestionNext();
            }
        }

        public bool ShowSingleObs
        {
            get { return _showSingleObs; }
            set
            {
                _showSingleObs = value;
                RaisePropertyChanged(() => ShowSingleObs);
                RaisePropertyChanged(() => ShowSingleObsValue);
            }
        }

        public bool ShowSingleObsList
        {
            get { return _showSingleObsList; }
            set
            {
                _showSingleObsList = value;
                RaisePropertyChanged(() => ShowSingleObsList);
                RaisePropertyChanged(() => ShowSingleObsListValue);
            }
        }

        public bool ShowTextObs
        {
            get { return _showTextObs; }
            set
            {
                _showTextObs = value;
                RaisePropertyChanged(() => ShowTextObs);
                RaisePropertyChanged(() => ShowTextObsValue);
            }
        }

        public bool ShowNumericObs
        {
            get { return _showNumericObs; }
            set
            {
                _showNumericObs = value;
                RaisePropertyChanged(() => ShowNumericObs);
                RaisePropertyChanged(() => ShowNumericObsValue);
            }
        }

        public bool ShowMultiObs
        {
            get { return _showMultiObs; }
            set
            {
                _showMultiObs = value;
                RaisePropertyChanged(() => ShowMultiObs);
                RaisePropertyChanged(() => ShowMultiObsValue);
            }
        }

        public string ShowSingleObsValue
        {
            get { return ShowSingleObs ? "visible" : "gone"; }
        }

        public string ShowSingleObsListValue
        {
            get { return ShowSingleObsList ? "visible" : "gone"; }
        }

        public string ShowTextObsValue
        {
            get { return ShowTextObs ? "visible" : "gone"; }
        }

        public string ShowNumericObsValue
        {
            get { return ShowNumericObs ? "visible" : "gone"; }
        }

        public string ShowMultiObsValue
        {
            get { return ShowMultiObs ? "visible" : "gone"; }
        }

        public void MoveToQuestionNext()
        {
            QuestionTemplateWrap.MoveToNextQuestion(this);
        }

        public object GetResponse()
        {
            if (ShowTextObs) // Text
                return ResponseText;
            if (ShowSingleObs) //"Single";
                return SelectedSingleOption.Id;
            if (ShowNumericObs) //"Numeric";
                return ResponseNumeric;
            if (ShowMultiObs) //"Multi";
            {
                var options = MultiOptions.Where(x => x.Selected).ToList();
                return string.Join(",", options);
            }

            return null;
        }

        public QuestionTemplate(Question question,  string response = "")
        {
            
            Id = question.Id;
            Display = question.ToString();
            Concept = question.Concept;
            Allow = false;
            ShowTextObs = Concept.ConceptTypeId == "Text";
            ShowSingleObs = Concept.ConceptTypeId == "Single";
            ShowNumericObs = Concept.ConceptTypeId == "Numeric";
            ShowMultiObs = Concept.ConceptTypeId == "Multi";

            //ResponseText = response;

            //TODO:ShowSingleObsList

            if (ShowSingleObs)
            {
                SingleOptions = SingleOptionsList = Concept.Category.Items;
            }

            if (ShowMultiObs)
                MultiOptions = Concept.Category.Items;
        }
    }
}