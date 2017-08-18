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
        private List<CategoryItem> _selectedMultiOptions=new List<CategoryItem>();
        private bool _showSingleObs;
        private bool _showSingleObsList;
        private bool _showTextObs;
        private bool _showNumericObs;
        private bool _showMultiObs;
        private decimal _responseNumeric;
        private bool _allow;
        private string _errorSummary;
        private string _selection;
        private IMvxCommand _checkOptionCommand;

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
                        categoryItem.Allow = Allow;
                    }
                    SingleOptions=new List<CategoryItem>(SingleOptions);
                }
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
            set
            {
                _errorSummary = value; RaisePropertyChanged(() => ErrorSummary);
                if (!string.IsNullOrWhiteSpace(ErrorSummary))
                {
                    QuestionTemplateWrap.Parent.FormError = "Please fix all errors before saving";
                }
                else
                {
                    QuestionTemplateWrap.Parent.FormError = string.Empty;
                }
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

        public string Selection
        {
            get { return _selection; }
            set { _selection = value; RaisePropertyChanged(() => Selection); }
        }

        public List<CategoryItem> MultiOptions
        {
            get { return _multiOptions; }
            set
            {
                _multiOptions = value;
                RaisePropertyChanged(() => MultiOptions);
                foreach (var option in _multiOptions)
                {
                    option.OptionSelected += MultiOption_OptionSelected; 
                }
            }
        }

        private void MultiOption_OptionSelected(object sender, Core.Event.OptionSelectedEventArgs e)
        {
            AddOrUpdateMultiSelection(e.CategoryItem);
        }

        private void AddOrUpdateMultiSelection(CategoryItem option)
        {
            var multiOptions = SelectedMultiOptions.ToList();

            if (multiOptions.Any(x => x.Id == option.Id))
            {
                var optionForUpdate = multiOptions.First(x => x.Id == option.Id);
                multiOptions.Remove(optionForUpdate);
                optionForUpdate.Selected = option.Selected;
                multiOptions.Add(optionForUpdate);
            }
            else
            {
              var newItem = MultiOptions.First(x => x.Id == option.Id);
                multiOptions.Add(newItem);
            }

            SelectedMultiOptions = multiOptions;
        }


        public List<CategoryItem> SelectedMultiOptions
        {
            get { return _selectedMultiOptions; }
            set
            {
                _selectedMultiOptions = value;
                RaisePropertyChanged(() => SelectedMultiOptions);

                var selectedOptions = SelectedMultiOptions.Where(x => x.Selected).ToList();
                if (selectedOptions.Count > 0)
                {
                    Selection = string.Join(",", selectedOptions);
                }
                else
                {
                    Selection = string.Empty;

                }
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
                return SelectedSingleOption.ItemId;
            if (ShowNumericObs) //"Numeric";
                return ResponseNumeric;
            if (ShowMultiObs) //"Multi";
            {
                var options = MultiOptions.Where(x => x.Selected).ToList();
                return string.Join(",", options.Select(x=>x.ItemId));
            }

            return null;
        }

        public void SetResponse(object response)
        {
            if (ShowTextObs) // Text
                ResponseText = response.ToString();
            if (ShowSingleObs) //"Single";
            {
                var text = response.ToString();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    var option = SingleOptions.FirstOrDefault(x => x.ItemId == new Guid(text));
                    SelectedSingleOption = option;
                }
            }
            if (ShowNumericObs) //"Numeric";
            {
                var text = response.ToString();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    decimal numeric;
                    decimal.TryParse(text, out numeric);
                    ResponseNumeric = numeric;
                }
            }
            if (ShowMultiObs) //"Multi";
            {
                var itemIds=new List<Guid>();
                var ids = response.ToString().Split(',');
                foreach (var id in ids)
                {
                    itemIds.Add(new Guid(id));
                }
                if (itemIds.Count > 0)
                {
                    var multiOptions=new List<CategoryItem>();
                    var options = MultiOptions
                        .Where(
                            x => itemIds.Contains(x.ItemId) &&
                                 x.ItemId != Guid.Empty)
                        .ToList();

                    foreach (var categoryItem in MultiOptions)
                    {
                        
                        if (options.Any(x => x.ItemId == categoryItem.ItemId))
                        {
                            if (!categoryItem.Selected)
                                categoryItem.Selected = true;
                        }                            
                        multiOptions.Add(categoryItem);
                    }
                    MultiOptions = multiOptions;
                }                                    
            }

        }

        public IMvxCommand CheckOptionCommand
        {
            get
            {
                _checkOptionCommand = _checkOptionCommand ?? new MvxCommand(CheckOption);
                return _checkOptionCommand;
            }
        }

        private void CheckOption()
        {
            throw new NotImplementedException();
        }


        public QuestionTemplate(Question question,  string response = "")
        {            
            Id = question.Id;
            Display = question.ToString();
            Concept = question.Concept;
            Allow = true;
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
            {
                var options= Concept.Category.Items;
                options.Add(CategoryItem.CreateInitial("[Select Options]"));
                MultiOptions = options.OrderBy(x => x.Rank).ToList();
            }
        }
    }
}