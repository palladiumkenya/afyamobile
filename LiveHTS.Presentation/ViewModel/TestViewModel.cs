using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmValidation;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class TestViewModel : MvxViewModel, ITestViewModel
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
        
        private Guid _encounterId;
        private string _errorSummary;
        private string _resultCode;
        private ObsTestResult _testResult;
        private IMvxCommand _saveTestCommand;
        

        private readonly IHIVTestingService _testingService;
        private readonly ISettings _settings;
        
        
        private ITestEpisodeViewModel _parent;
        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { _editMode = value; }
        }

        public ITestEpisodeViewModel Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;

                TestName = Parent.TestName;
                EncounterId = Parent.Parent.Encounter.Id;
            }
        }

        public string ErrorSummary
        {
            get { return _errorSummary; }
            set { _errorSummary = value; RaisePropertyChanged(() => ErrorSummary); }
        }

        public ValidationHelper Validator { get; }
        public ObservableDictionary<string, string> Errors { get; set; }

        public ObsTestResult TestResult
        {
            get { return _testResult; }
            set { _testResult = value; RaisePropertyChanged(() => TestResult);}
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

        public string ResultCode
        {
            get { return _resultCode; }
            set { _resultCode = value; RaisePropertyChanged(() => ResultCode);}
        }

        public IMvxCommand SaveTestCommand
        {
            get
            {
                _saveTestCommand = _saveTestCommand ?? new MvxCommand(SaveTest, CanSaveTest);
                return _saveTestCommand;
            }
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
                {
                    Result = SelectedResult.ItemId;
                    ResultCode = null == SelectedResult.Item ? string.Empty : SelectedResult.Item.Code;
                }
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

        public TestViewModel()
        {
            Validator = new ValidationHelper();
            Expiry=DateTime.Today;
            
            _testingService =  Mvx.Resolve<IHIVTestingService>();
            _settings = Mvx.Resolve<ISettings>();

            var kitsJson = _settings.GetValue("lookup.KitName", "");
            var resultsJson = _settings.GetValue("lookup.TestResult", "");

            if (!string.IsNullOrWhiteSpace(kitsJson))
            {
                Kits = JsonConvert.DeserializeObject<List<CategoryItem>>(kitsJson);
            }
            if (!string.IsNullOrWhiteSpace(resultsJson))
            {
                Results = JsonConvert.DeserializeObject<List<CategoryItem>>(resultsJson);
            }
          
        }

        private void LoadTest()
        {
            if (null != TestResult)
            {
                Id = TestResult.Id;
                SelectedKit = Kits.FirstOrDefault(x=>x.ItemId== TestResult.Kit);
                KitOther = TestResult.KitOther;
                LotNumber = TestResult.LotNumber;
                Expiry = TestResult.Expiry;
                SelectedResult = Results.FirstOrDefault(x => x.ItemId == TestResult.Result);
            }
        }

        private void Clear()
        {
            ShowKitOther = false;
            Expiry = DateTime.Today;
            KitOther =LotNumber=String.Empty;
            SelectedKit = Kits.OrderBy(x => x.Rank).FirstOrDefault();
            SelectedResult = Results.OrderBy(x => x.Rank).FirstOrDefault();
        }

        public void Init(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;
        }

        public override void ViewAppeared()
        {
            // Load Client

            var kitsJson = _settings.GetValue("lookup.KitName", "");
            var resultsJson = _settings.GetValue("lookup.TestResult", "");

            if (!string.IsNullOrWhiteSpace(kitsJson))
            {
                Kits = JsonConvert.DeserializeObject<List<CategoryItem>>(kitsJson);
            }
            if (!string.IsNullOrWhiteSpace(resultsJson))
            {
                Results = JsonConvert.DeserializeObject<List<CategoryItem>>(resultsJson);
            }

            if (!EditMode)
            {
                Clear();
            }
            else
            {
                _testResult = Parent.Test;
                LoadTest();
            }

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

            Validator.AddRule(
                nameof(Result),
                () => RuleResult.Assert(
                    !Result.IsNullOrEmpty(),
                    $"{nameof(Result)} is required"
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

        private void SaveTest()
        {
            if (Validate())
            {
                TestResult= GenerateTest();
                _testingService.SaveTest(TestResult);
                Parent.Parent.Referesh(TestResult.EncounterId);
                Parent.CloseTestCommand.Execute();
            }
        }
     

        public bool CanSaveTest()
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
            var obs= ObsTestResult.Create(TestName,Attempt,Kit,KitOther,LotNumber,Expiry,Result,EncounterId,ResultCode);
            if (EditMode)
                obs.Id = Id;

            obs.IsValid = false;
            if (null != SelectedResult.Item)
            {
                obs.IsValid = SelectedResult.Item.Code != "I";
            }
            return obs;
        }
    }
}