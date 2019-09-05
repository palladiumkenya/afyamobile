using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class MemberTracingViewModel: MvxViewModel,IMemberTracingViewModel
    {
        private ObsFamilyTraceResult _trace;

        private  IMvxCommand _addTraceCommand;
        private Action _addTraceCommandAction;
        private readonly ISettings _settings;
        private readonly IDialogService _dialogService;
        private readonly IMemberTracingService _tracingService;

        private Guid _encounterTypeId;
        private Client _client;
        private Encounter _encounter;
        private List<FamilyTraceTemplateWrap> _traces;
        private Action _editTestCommandAction;
        private Action _closeTestCommandAction;
        private MvxCommand _closeTestCommand;
        private IDashboardService _dashboardService;
        private ILookupService _lookupService;
        private IndexClientDTO _indexClient;

        public MemberTracingViewModel(ISettings settings, IDialogService dialogService, IMemberTracingService tracingService, IDashboardService dashboardService, ILookupService lookupService)
        {
            _settings = settings;
            _dialogService = dialogService;
            _tracingService = tracingService;
            _dashboardService = dashboardService;
            _lookupService = lookupService;
        }

        public void Init(string formId, string encounterTypeId, string mode, string clientId, string encounterId,string indexclient)
        {

            // Load Client

            Client = _dashboardService.LoadClient(new Guid(clientId));

            if (null != Client)
            {
                var clientJson = JsonConvert.SerializeObject(Client);
                _settings.AddOrUpdateValue("client", clientJson);
            }
            if (!string.IsNullOrWhiteSpace(indexclient))
            {
                IndexClient = new IndexClientDTO(new Guid(indexclient));
                _settings.AddOrUpdateValue("mclientIndex", JsonConvert.SerializeObject(IndexClient));
            }
            // Load or Create Encounter

            if (!string.IsNullOrWhiteSpace(encounterTypeId))
            {
                _settings.AddOrUpdateValue("encounterTypeId", encounterTypeId);
            }

            EncounterTypeId = new Guid(encounterTypeId);

            if (mode == "new")
            {
                //  New Encounter
                _settings.AddOrUpdateValue("client.link.mode", "new");
                Encounter = _tracingService.StartEncounter(new Guid(formId), EncounterTypeId, Client.Id, AppProviderId, AppUserId, AppPracticeId, AppDeviceId,IndexClient.Id);
            }
            else
            {
                //  Load Encounter
                _settings.AddOrUpdateValue("client.link.mode", "open");
                Encounter = _tracingService.OpenEncounter(new Guid(encounterId));
            }

            if (null == Encounter)
            {
                throw new ArgumentException("Encounter has not been Initialized");
            }
            //Store Encounter

            var encounterJson = JsonConvert.SerializeObject(Encounter);
            _settings.AddOrUpdateValue("client.encounter", encounterJson);




            var modes = _lookupService.GetCategoryItems("TraceMode", true, "[Select Mode]").ToList();
            _settings.AddOrUpdateValue("lookup.TMode", JsonConvert.SerializeObject(modes));
            var outcomes = _lookupService.GetCategoryItems("PNSOutcome", true, "[Select Outcome]").ToList();
            _settings.AddOrUpdateValue("lookup.TOutcome", JsonConvert.SerializeObject(outcomes));
            var consents = _lookupService.GetCategoryItems("YesNo", true, "[Select Consent]").ToList();
            _settings.AddOrUpdateValue("lookup.TConsent", JsonConvert.SerializeObject(consents));

            var reasons = _lookupService.GetCategoryItems("ReasonNotContacted", true, "[Select Reason]").ToList();
            _settings.AddOrUpdateValue("lookup.ReasonNotContacted", JsonConvert.SerializeObject(reasons));



        }

        public override void ViewAppeared()
        {

            var clientJson = _settings.GetValue("client.dto", "");
            var indexClientJson = _settings.GetValue("mclientIndex", "");
            var clientEncounterJson = _settings.GetValue("client.encounter", "");
            var encounterTypeId = _settings.GetValue("encounterTypeId", "");

            if (null == Client && !string.IsNullOrWhiteSpace(clientJson))
            {
                Client = JsonConvert.DeserializeObject<Client>(clientJson);
            }

            if (null == IndexClient && !string.IsNullOrWhiteSpace(indexClientJson))
            {
                IndexClient = JsonConvert.DeserializeObject<IndexClientDTO>(indexClientJson);
            }

            if (EncounterTypeId.IsNullOrEmpty() && !string.IsNullOrWhiteSpace(encounterTypeId))
            {
                EncounterTypeId = new Guid(encounterTypeId);
            }


            if (null == Encounter && !string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                Encounter = JsonConvert.DeserializeObject<Encounter>(clientEncounterJson);
            }
        }

        public Guid AppUserId
        {
            get { return GetGuid("livehts.userid"); }
        }

        public Guid AppProviderId
        {
            get { return GetGuid("livehts.providerid"); }
        }

        public Guid AppPracticeId
        {
            get { return GetGuid("livehts.practiceid"); }
        }

        public Guid AppDeviceId
        {
            get { return GetGuid("livehts.deviceid"); }
        }

        public Guid EncounterTypeId
        {
            get { return _encounterTypeId; }
            set { _encounterTypeId = value; }
        }

        public IndexClientDTO IndexClient
        {
            get { return _indexClient; }
            set { _indexClient = value; RaisePropertyChanged(() => IndexClient); }
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
        private void LoadTraces()
        {

            var modesJson = _settings.GetValue("lookup.TMode", "");
            var outcomeJson = _settings.GetValue("lookup.TOutcome", "");
            var consentJson = _settings.GetValue("lookup.TConsent", "");
            var reasonJson = _settings.GetValue("lookup.ReasonNotContacted", "");


            List<CategoryItem> modes = new List<CategoryItem>();
            if (!string.IsNullOrWhiteSpace(modesJson))
            {
                modes = JsonConvert.DeserializeObject<List<CategoryItem>>(modesJson);
            }

            List<CategoryItem> outcomes = new List<CategoryItem>();
            if (!string.IsNullOrWhiteSpace(outcomeJson))
            {
                outcomes = JsonConvert.DeserializeObject<List<CategoryItem>>(outcomeJson);
            }

            List<CategoryItem> consents = new List<CategoryItem>();
            if (!string.IsNullOrWhiteSpace(consentJson))
            {
                consents = JsonConvert.DeserializeObject<List<CategoryItem>>(consentJson);
            }

            List<CategoryItem> reasons=new List<CategoryItem>();
            if ( !string.IsNullOrWhiteSpace(reasonJson))
            {
                reasons = JsonConvert.DeserializeObject<List<CategoryItem>>(reasonJson);
            }



            if (null != Encounter)
            {
                Traces = ConvertToTraceWrapperClass(this, Encounter, modes, outcomes,consents,reasons);
            }
        }

        private static List<FamilyTraceTemplateWrap> ConvertToTraceWrapperClass(IMemberTracingViewModel clientDashboardViewModel, Encounter encounter, List<CategoryItem> modes, List<CategoryItem> outcomes, List<CategoryItem> consents,List<CategoryItem> reasons)
        {
            List<FamilyTraceTemplateWrap> list = new List<FamilyTraceTemplateWrap>();

            var testResults = encounter.ObsFamilyTraceResults.ToList();

            foreach (var r in testResults)
            {
                list.Add(new FamilyTraceTemplateWrap(clientDashboardViewModel, new FamilyTraceTemplate(r, modes, outcomes, consents,reasons)));
            }

            return list;
        }
        public ObsFamilyTraceResult Trace
        {
            get { return _trace; }
            set
            {
                _trace = value;
                RaisePropertyChanged(() => Trace);
            }
        }
        public List<FamilyTraceTemplateWrap> Traces
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
        public IMvxCommand CloseTestCommand
        {
            get
            {
                _closeTestCommand = _closeTestCommand ?? new MvxCommand(CloseTest);
                return _closeTestCommand;
            }
        }

        private void CloseTest()
        {
            CloseTestCommandAction?.Invoke();
        }

        public Action AddTraceCommandAction
        {
            get { return _addTraceCommandAction; }
            set { _addTraceCommandAction = value; }
        }

        public void SaveTrace(ObsFamilyTraceResult test)
        {
            _tracingService.SaveTest(test, Client.Id,IndexClient.Id);
            _tracingService.MarkEncounterCompleted(test.EncounterId,AppUserId, true);
            Encounter = _tracingService.OpenEncounter(Encounter.Id);
        }

        public async void DeleteTrace(ObsFamilyTraceResult test)
        {
            if (null != test)
            {
                var result = await _dialogService.ConfirmAction("Are you sure ?", "Delete this Trace");
                if (result)
                {

                    _tracingService.DeleteTest(test, Client.Id,IndexClient.Id);
                    Referesh(test.EncounterId);
                }
            }
        }

        public void EditTrace(ObsFamilyTraceResult testResult)
        {
            Trace = testResult;
            EditTestCommandAction?.Invoke();
        }
        public Action EditTestCommandAction
        {
            get { return _editTestCommandAction; }
            set { _editTestCommandAction = value; }
        }

        public Action CloseTestCommandAction
        {
            get { return _closeTestCommandAction; }
            set { _closeTestCommandAction = value; }
        }
        public void Referesh(Guid encounterId)
        {
            Encounter = _tracingService.OpenEncounter(encounterId);
        }

        private void AddTrace()
        {
            AddTraceCommandAction?.Invoke();
        }
        private bool CanAddTrace()
        {
            return true;
        }

        public Guid GetGuid(string key)
        {
            var guid = _settings.GetValue(key, "");

            if (string.IsNullOrWhiteSpace(guid))
                return Guid.Empty;

            return new Guid(guid);
        }
    }
}
