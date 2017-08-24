using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class HIVTestTemplate: MvxNotifyPropertyChanged, IHIVTestTemplate
    {
        

        private Guid _id;
        private string _testName;
        private int _attempt;
        private Guid _kit;
        private string _kitOther;
        private string _lotNumber;
        private DateTime _expiry;
        private Guid _result;
        private List<CategoryItem> _kits=new List<CategoryItem>();
        private List<CategoryItem> _results=new List<CategoryItem>();
        private bool _showKitOther;
        private CategoryItem _selectedKit;
        private CategoryItem _selectedResult;
        private IHIVTestTemplateWrap _hivTestTemplateWrap;
        private Guid _encounterId;
        private string _errorSummary;


        public string ErrorSummary
        {
            get { return _errorSummary; }
            set { _errorSummary = value; RaisePropertyChanged(() => ErrorSummary); }
        }

        public ValidationHelper Validator { get; }
        public ObservableDictionary<string, string> Errors { get; set; }

        public ObsTestResult TestResult
        {
            get { return GenerateTest(); }
        }

      

        public IHIVTestTemplateWrap HIVTestTemplateWrap
        {
            get { return _hivTestTemplateWrap; }
            set { _hivTestTemplateWrap = value; }
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(() => Id);}
        }

        public string TestName
        {
            get { return _testName; }
            set { _testName = value; RaisePropertyChanged(() => TestName); }
        }

        public int Attempt
        {
            get { return _attempt; }
            set { _attempt = value; RaisePropertyChanged(() => Attempt); }
        }

        public Guid Kit
        {
            get { return _kit; }
            set { _kit = value; RaisePropertyChanged(() => Kit); }
        }

        public bool ShowKitOther
        {
            get { return _showKitOther; }
            set { _showKitOther = value; RaisePropertyChanged(() => ShowKitOther); }
        }

        public string KitOther
        {
            get { return _kitOther; }
            set { _kitOther = value; RaisePropertyChanged(() => KitOther); }
        }

        public string LotNumber
        {
            get { return _lotNumber; }
            set { _lotNumber = value; RaisePropertyChanged(() => LotNumber); }
        }

        public DateTime Expiry
        {
            get { return _expiry; }
            set { _expiry = value; RaisePropertyChanged(() => Expiry); }
        }

        public Guid Result
        {
            get { return _result; }
            set { _result = value; RaisePropertyChanged(() => Result); }
        }

        public Guid EncounterId
        {
            get { return _encounterId; }
            set { _encounterId = value; RaisePropertyChanged(() => EncounterId); }
        }

        public CategoryItem SelectedKit
        {
            get { return _selectedKit; }
            set
            {
                _selectedKit = value;
                RaisePropertyChanged(() => SelectedKit);
                if (null != SelectedKit)
                    Kit = SelectedKit.ItemId;
                ShowOther();
            }
        }

        public CategoryItem SelectedResult
        {
            get { return _selectedResult; }
            set
            {
                _selectedResult = value; RaisePropertyChanged(() => SelectedResult);
                if (null != SelectedResult)
                    Result = SelectedResult.ItemId;
            }
        }

        public List<CategoryItem> Kits
        {
            get { return _kits; }
            set { _kits = value; RaisePropertyChanged(() => Kits); }
        }

        public List<CategoryItem> Results
        {
            get { return _results; }
            set { _results = value; RaisePropertyChanged(() => Results); }
        }

       

        public HIVTestTemplate(ObsTestResult testResult, List<CategoryItem> kits, List<CategoryItem> results)
        {
            Validator = new ValidationHelper();

            Kits = kits;
            Results = results;

            if (null != Kits && Kits.Count > 0)
            {
                var kit = Kits.FirstOrDefault(x => x.ItemId == testResult.Kit);
                if(null!=kit)
                {

                    SelectedKit = kit;
                }
                else
                {
                    SelectedKit = Kits.OrderBy(x => x.Rank).First();
                }
            }

            if (null != Results && Results.Count > 0)
            {
                var result = Results.FirstOrDefault(x => x.ItemId == testResult.Result);
                if (null != result)
                {

                    SelectedResult = result;
                }
                else
                {
                    SelectedResult = Results.OrderBy(x => x.Rank).First();
                }
                
            }

            Id = testResult.Id;
            TestName = testResult.TestName;
            Attempt = testResult.Attempt;
            KitOther = testResult.KitOther;
            LotNumber = testResult.LotNumber;
            Expiry = testResult.Expiry;
            EncounterId = testResult.EncounterId;
        }

        public bool Validate()
        {
            ErrorSummary=string.Empty;

            Validator.AddRule(
                nameof(Kit),
                () => RuleResult.Assert(
                    !Kit.IsNullOrEmpty(),
                    $"{nameof(Kit)} is required"
                )
            );

            if (ShowKitOther)
            {
                Validator.AddRule(
                    nameof(KitOther),
                    () => RuleResult.Assert(
                        !string.IsNullOrWhiteSpace(KitOther),
                        $"Specify Other Kit"
                    )
                );
            }

            Validator.AddRule(
                nameof(LotNumber),
                () => RuleResult.Assert(
                    !string.IsNullOrWhiteSpace(LotNumber),
                    $"{nameof(LotNumber)} is required"
                )
            );

            Validator.AddRule(
                nameof(Expiry),
                () => RuleResult.Assert(
                    Expiry > DateTime.Today,
                    $"{nameof(Expiry)} should be a valid date"
                )
            );
            
            var result = Validator.ValidateAll();
            Errors = result.AsObservableDictionary();
            if (null != Errors && Errors.Count > 0)
            {
                ErrorSummary = Errors.First().Value;
            }
            return result.IsValid;
        }


        public bool CanSave()
        {
            return true;
        }

        public bool CanDelete()
        {
            return true;
        }


        private void ShowOther()
        {
            ShowKitOther = false;
            if (null != SelectedKit && 
                !SelectedKit.ItemId.IsNullOrEmpty()&&
                SelectedKit.Item.Display.ToLower().Contains("other".ToLower()))
            {
                ShowKitOther = true;
            }
        }
        private ObsTestResult GenerateTest()
        {
            return ObsTestResult.Create(Id,TestName,Attempt,Kit,KitOther,LotNumber,Expiry,Result,EncounterId);
        }
    }
}