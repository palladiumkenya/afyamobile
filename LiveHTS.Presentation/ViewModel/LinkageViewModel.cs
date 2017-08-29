using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class LinkageViewModel:MvxViewModel,ILinkageViewModel
    {
        private readonly ISettings _settings;
        private readonly IDashboardService _dashboardService;
        private readonly ILookupService _lookupService;
        private readonly ILinkageService _testingService;

        private string _referredTo;
        private DateTime? _datePromised;
        private string _facilityHandedTo;
        private string _handedTo;
        private string _workerCarde;
        private DateTime? _dateEnrolled;
        private string _enrollmentId;
        private string _remarks;
        private List<TraceTemplateWrap> _traces;
        private IMvxCommand _addTraceCommand;
        private TraceDateDTO _selectedDate;
        private Guid _encounterTypeId;
        private Client _client;
        private Encounter _encounter;

        public Guid EncounterTypeId
        {
            get { return _encounterTypeId; }
            set { _encounterTypeId = value; }
        }

        public Client Client
        {
            get { return _client; }
            set { _client = value; RaisePropertyChanged(() => Client); }
        }
        public Encounter Encounter
        {
            get { return _encounter; }
            set
            {
                _encounter = value; RaisePropertyChanged(() => Encounter);
                LoadTraces();
            }
        }

        public string ReferredTo
        {
            get { return _referredTo; }
            set { _referredTo = value; RaisePropertyChanged(() => ReferredTo);}
        }

        public DateTime? DatePromised
        {
            get { return _datePromised; }
            set { _datePromised = value; RaisePropertyChanged(() => DatePromised); }
        }

        public string FacilityHandedTo
        {
            get { return _facilityHandedTo; }
            set { _facilityHandedTo = value; RaisePropertyChanged(() => FacilityHandedTo); }
        }

        public string HandedTo
        {
            get { return _handedTo; }
            set { _handedTo = value; RaisePropertyChanged(() => HandedTo); }
        }

        public string WorkerCarde
        {
            get { return _workerCarde; }
            set { _workerCarde = value; RaisePropertyChanged(() => WorkerCarde); }
        }

        public DateTime? DateEnrolled
        {
            get { return _dateEnrolled; }
            set { _dateEnrolled = value; RaisePropertyChanged(() => DateEnrolled); }
        }

        public string EnrollmentId
        {
            get { return _enrollmentId; }
            set { _enrollmentId = value; RaisePropertyChanged(() => EnrollmentId); }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; RaisePropertyChanged(() => Remarks); }
        }

        public List<TraceTemplateWrap> Traces
        {
            get { return _traces; }
            set
            {
                _traces = value; RaisePropertyChanged(() => Traces);
                AddTraceCommand.RaiseCanExecuteChanged();
            }
        }

        public IMvxCommand AddTraceCommand
        {
            get
            {
                _addTraceCommand = _addTraceCommand ?? new MvxCommand(AddTrace, CanAddTrace);
                return _addTraceCommand;
            }
        }

        private void AddTrace()
        {
            throw new NotImplementedException();
        }

        private bool CanAddTrace()
        {
            throw new NotImplementedException();
        }

        public void RemoveTrace(TraceTemplate template)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ChangedDateEvent> ChangedDate;

        public TraceDateDTO SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
                UpdateExpiryDate(SelectedDate);
            }
        }
        public LinkageViewModel(ILookupService lookupService, IDashboardService dashboardService, ILinkageService testingService, ISettings settings)
        {
            _lookupService = lookupService;
            _dashboardService = dashboardService;
            _testingService = testingService;
            _settings = settings;
        }


        public void Init(string formId, string encounterTypeId, string mode, string clientId, string encounterId)
        {
         
            // Load Client
            Client = _dashboardService.LoadClient(new Guid(clientId));

            // Load or Create Encounter

            EncounterTypeId = new Guid(encounterTypeId);

            if (mode == "new")
            {
                //  New Encounter
                _settings.AddOrUpdateValue("client.test.mode", "new");
                Encounter = _testingService.StartEncounter(new Guid(formId), EncounterTypeId, Client.Id, Guid.Empty, Guid.Empty);
            }
            else
            {
                //  Load Encounter
                _settings.AddOrUpdateValue("client.test.mode", "open");
                Encounter = _testingService.OpenEncounter(Encounter.Id);
            }

            if (null == Encounter)
            {
                throw new ArgumentException("Encounter has not been Initialized");
            }

            //RaisePropertyChanged(() => FirstHIVTestViewModel.FirstTestName);
        }
        public void ShowDatePicker(Guid refId, DateTime refDate)
        {
            OnChangedDate(new ChangedDateEvent(refId, refDate));
        }

        public void SaveTrace(ObsTraceResult test)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrace(ObsTraceResult test)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnChangedDate(ChangedDateEvent e)
        {
            ChangedDate?.Invoke(this, e);
        }
        private void LoadTraces()
        {
            /*
            var kits = _lookupService.GetCategoryItems("KitName", true, "[Select Kit]").ToList();
            var results = _lookupService.GetCategoryItems("TestResult", true, "[Select Result]").ToList();

            if (null != Encounter)
            {
                FirstHIVTestViewModel.FirstTests = ConvertToHIVTestWrapperClass(this, Encounter, FirstHIVTestViewModel.FirstTestName, kits, results);

                var finalTestResult = Encounter.ObsFinalTestResults.ToList().FirstOrDefault();

                if (null != finalTestResult)
                {
                    var result = FirstHIVTestViewModel.FirstTestResults.FirstOrDefault(x => x.ItemId == finalTestResult.FirstTestResult);
                    if (null != result)
                    {
                        FirstHIVTestViewModel.SelectedFirstTestResult = result;
                    }
                    else
                    {
                        FirstHIVTestViewModel.SelectedFirstTestResult = FirstHIVTestViewModel.FirstTestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                }

                SecondHIVTestViewModel.SecondTests = ConvertToHIVTestWrapperClass(this, Encounter, SecondHIVTestViewModel.SecondTestName, kits, results);

                var finalSecondResult = Encounter.ObsFinalTestResults.ToList().FirstOrDefault();

                if (null != finalSecondResult)
                {
                    var result = SecondHIVTestViewModel.SecondTestResults.FirstOrDefault(x => x.ItemId == finalSecondResult.SecondTestResult);
                    if (null != result)
                    {
                        SecondHIVTestViewModel.SelectedSecondTestResult = result;
                    }
                    else
                    {
                        SecondHIVTestViewModel.SelectedSecondTestResult = SecondHIVTestViewModel.SecondTestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                }


                var finalResult = Encounter.ObsFinalTestResults.ToList().FirstOrDefault();

                if (null != finalResult)
                {
                    var result = FinalTestResults.FirstOrDefault(x => x.ItemId == finalResult.EndResult);
                    if (null != result)
                    {
                        SelectedFinalTestResult = result;
                    }
                    else
                    {
                        SelectedFinalTestResult = FinalTestResults.OrderBy(x => x.Rank).FirstOrDefault();
                    }
                }
            }

            */
        }


        private void UpdateExpiryDate(TraceDateDTO selectedDate)
        {
            var test1 = Traces.FirstOrDefault(x => x.TraceTemplate.Id == selectedDate.Id);
            if (null != test1)
            {
                test1.TraceTemplate.Date= selectedDate.EventDate;
            }
        }
    }
}