using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class QuestionTemplate : MvxViewModel, IQuestionTemplate
    {
        private Guid _id;
        private string _display;
        private Concept _concept;
        private string _response;
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

        public string Response
        {
            get { return _response; }
            set
            {
                _response = value;
               RaisePropertyChanged(() => Response);
            }
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

        public QuestionTemplate(Question question, string response = "")
        {
            Id = question.Id;
            Display = question.ToString();
            Concept = question.Concept;

            ShowTextObs = Concept.ConceptTypeId == "Text";
            ShowSingleObs = Concept.ConceptTypeId == "Single";
            ShowNumericObs = Concept.ConceptTypeId == "Numeric";
            ShowMultiObs = Concept.ConceptTypeId == "Multi";

            Response = response;

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