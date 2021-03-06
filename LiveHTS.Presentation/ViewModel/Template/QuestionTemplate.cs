﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class QuestionTemplate : MvxNotifyPropertyChanged, IQuestionTemplate
    {
        
        private Guid _id;

        private string _display;
        private string _errorSummary;

        private string _responseText;
        private bool _showTextObs;
        private decimal _responseNumeric;
        private bool _showNumericObs;

        private ObservableCollection<CategoryItem> _singleOptions = new ObservableCollection<CategoryItem>();
        private CategoryItem _selectedSingleOption;
        private bool _showSingleObs;

        private string _selection;
        private List<CategoryItem> _multiOptions = new List<CategoryItem>();
        private List<CategoryItem> _selectedMultiOptions = new List<CategoryItem>();
        private bool _showMultiObs;
        
        private Concept _concept;
        private bool _allow;
        private string _otherSingleResponseText;
        private bool _showOtherSingleObs;
        private string _otherMultiResponseText;
        private bool _showOtherMultiObs;
        private DateTime _responseDate;
        private bool _showDateObs;
        private int _scaling;
        private bool _required;
        private bool _requiresResponse;


        public IQuestionTemplateWrap QuestionTemplateWrap { get; set; }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(() => Id); }
        }
        public string Display
        {
            get { return _display; }
            set { _display = value; RaisePropertyChanged(() => Display); }
        }
        public string ErrorSummary
        {
            get { return _errorSummary; }
            set
            {
                _errorSummary = value;
                RaisePropertyChanged(() => ErrorSummary);

                QuestionTemplateWrap.Parent.FormError = !string.IsNullOrWhiteSpace(ErrorSummary) ? "Please fill all required fields" : string.Empty;
            }
        }

        public string ResponseText
        {
            get { return _responseText; }
            set
            {
                _responseText = value;
                RaisePropertyChanged(() => ResponseText);
                AllowNextQuestion();
            }
        }
        public bool ShowTextObs
        {
            get { return _showTextObs; }
            set { _showTextObs = value; RaisePropertyChanged(() => ShowTextObs); }
        }

        public decimal ResponseNumeric
        {
            get { return _responseNumeric; }
            set { _responseNumeric = value; RaisePropertyChanged(() => ResponseNumeric); AllowNextQuestion(); }
        }
        public bool ShowNumericObs
        {
            get { return _showNumericObs; }
            set { _showNumericObs = value; RaisePropertyChanged(() => ShowNumericObs); }
        }

        public DateTime ResponseDate
        {
            get { return _responseDate; }
            set
            {
                _responseDate = value;
                RaisePropertyChanged(() => ResponseDate);
                AllowNextQuestion();
            }
        }

        public bool ShowDateObs
        {
            get { return _showDateObs; }
            set { _showDateObs = value; RaisePropertyChanged(() => ShowDateObs); }
        }


        public ObservableCollection<CategoryItem> SingleOptions
        {
            get { return _singleOptions; }
            set { _singleOptions = value; RaisePropertyChanged(() => SingleOptions); }
        }
        public CategoryItem SelectedSingleOption
        {
            get { return _selectedSingleOption; }
            set
            {
                _selectedSingleOption = value;
                RaisePropertyChanged(() => SelectedSingleOption);
                AllowNextQuestion();
            }
        }
        public bool ShowSingleObs
        {
            get { return _showSingleObs; }
            set { _showSingleObs = value; RaisePropertyChanged(() => ShowSingleObs); }
        }
        public string OtherSingleResponseText
        {
            get { return _otherSingleResponseText; }
            set { _otherSingleResponseText = value; RaisePropertyChanged(() => OtherSingleResponseText); }
        }
        public bool ShowOtherSingleObs
        {
            get { return _showOtherSingleObs; }
            set { _showOtherSingleObs = value; RaisePropertyChanged(() => ShowOtherSingleObs); }
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
        public bool ShowMultiObs
        {
            get { return _showMultiObs; }
            set
            {
                _showMultiObs = value;
                RaisePropertyChanged(() => ShowMultiObs);
            }
        }
        public string OtherMultiResponseText
        {
            get { return _otherMultiResponseText; }
            set { _otherMultiResponseText = value; RaisePropertyChanged(() => OtherMultiResponseText); }
        }
        public bool ShowOtherMultiObs
        {
            get { return _showOtherMultiObs; }
            set { _showOtherMultiObs = value; RaisePropertyChanged(() => ShowOtherMultiObs); }
        }

        public List<CategoryItem> SelectedMultiOptions
        {
            get { return _selectedMultiOptions; }
            set
            {
                _selectedMultiOptions = value;
                RaisePropertyChanged(() => SelectedMultiOptions);

                var selectedOptions = SelectedMultiOptions.Where(x => x.Selected).ToList();
                Selection = selectedOptions.Count > 0 ? string.Join(",", selectedOptions) : string.Empty;
                AllowNextQuestion();
            }
        }

        public Concept Concept
        {
            get { return _concept; }
            set { _concept = value; RaisePropertyChanged(() => Concept); }
        }
        public bool Allow
        {
            get { return _allow; }
            set
            {
                _allow = value; RaisePropertyChanged(() => Allow);
                RequiresResponse = Allow && Required;
            }
        }

        public bool RequiresResponse
        {
            get { return _requiresResponse; }
            set { _requiresResponse = value; RaisePropertyChanged(() => RequiresResponse); }
        }

        public bool Required
        {
            get { return _required; }
            set { _required = value; RaisePropertyChanged(() => Required); }
        }

        public int Scaling
        {
            get { return _scaling; }
            set { _scaling = value; RaisePropertyChanged(() => Scaling); }
        }


        public QuestionTemplate(Question question, string response = "")
        {
            Scaling = 100;
            Id = question.Id;
            Display = question.ToString();
            Concept = question.Concept;
            Allow = true;
            Required = question.IsRequired;

            ShowTextObs = Concept.ConceptTypeId == "Text";
            ShowSingleObs = Concept.ConceptTypeId == "Single";
            ShowNumericObs = Concept.ConceptTypeId == "Numeric";
            ShowMultiObs = Concept.ConceptTypeId == "Multi";
            ShowDateObs = Concept.ConceptTypeId == "DateTime";

            if (ShowSingleObs|| ShowMultiObs)
            {
                var initialSelected = CategoryItem.CreateInitial("[Select Option]");
                var options = Concept.Category.Items;
                options.Add(initialSelected);
                options = options.OrderBy(x => x.Rank).ToList();

                if (ShowSingleObs)
                {
                    SingleOptions = new ObservableCollection<CategoryItem>(options);
                    _selectedSingleOption = initialSelected;
                }

                if (ShowMultiObs)
                {
                    MultiOptions = options;
                }
            }

            if (ShowDateObs)
            {
                Scaling = 200;
                _responseDate = DateTime.Today.Date.AddYears(1);
            }
        }

        public void AllowNextQuestion()
        {
            QuestionTemplateWrap.AllowNextQuestion(this);
        }

        public object GetResponse()
        {
            try
            {
                return ReadResponse();
            }
            catch (Exception e)
            {
                Mvx.Error(e.Message);
                return null;
            }
        }

        public void SetResponse(object response)
        {
            if (ShowTextObs)        // Text
                ResponseText = response.ToString();

            if (ShowSingleObs)      //"Single";
            {
                var text = response.ToString();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    var option = SingleOptions.FirstOrDefault(x => x.ItemId == new Guid(text));
                    SelectedSingleOption = option;
                }
            }

            if (ShowNumericObs)     //"Numeric";
            {
                var text = response.ToString();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    decimal numeric;
                    decimal.TryParse(text, out numeric);
                    ResponseNumeric = numeric;
                }
            }

            if (ShowMultiObs)       //"Multi";
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

            if (ShowDateObs)     //"Date";
            {
                var text = response.ToString();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    DateTime dateTime;
                    DateTime.TryParse(text, out dateTime);
                    ResponseDate= dateTime;
                }
            }


        }

        private object ReadResponse()
        {
            if (ShowTextObs)        // Text
                return ResponseText;

            if (ShowSingleObs) //"Single";
            {
                var selected = SelectedSingleOption.ItemId;
                if (selected.IsNullOrEmpty())
                    return null;
                return selected;
            }

            if (ShowNumericObs) //"Numeric";
            {
                var selected = ResponseNumeric;
                if (selected == 0)
                    return null;
                return selected;
            }

            if (ShowMultiObs)       //"Multi";
            {
                var options = MultiOptions.Where(x => x.Selected).ToList();
                return string.Join(",", options.Select(x => x.ItemId));
            }
            if (ShowDateObs)       //"Multi";
            {
                return ResponseDate;
            }
            return null;
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
    }
}