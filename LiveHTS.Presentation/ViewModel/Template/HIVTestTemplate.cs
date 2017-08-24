using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

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
                ShowOther();
            }
        }

        public CategoryItem SelectedResult
        {
            get { return _selectedResult; }
            set { _selectedResult = value; RaisePropertyChanged(() => SelectedResult);}
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

        public bool CanSave()
        {
            return true;
        }

        public bool CanDelete()
        {
            return true;
        }

        public HIVTestTemplate(ObsTestResult testResult, List<CategoryItem> kits, List<CategoryItem> results)
        {
            Kits = kits;
            Results = results;

            Id = testResult.Id;
            TestName = testResult.TestName;
            Attempt = testResult.Attempt;
            Kit = testResult.Kit;
            KitOther = testResult.KitOther;
            LotNumber = testResult.LotNumber;
            Expiry = testResult.Expiry;
            Result = testResult.Result;

            EncounterId = testResult.EncounterId;
        }
        private void ShowOther()
        {
            ShowKitOther = false;
            if (null != SelectedKit && SelectedKit.Item.Display.ToLower().Contains("other".ToLower()))
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