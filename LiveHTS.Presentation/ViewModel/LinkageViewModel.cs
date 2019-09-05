using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;
using MvvmValidation;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class LinkageViewModel:MvxViewModel,ILinkageViewModel
    {
        private readonly ISettings _settings;
        private readonly IDashboardService _dashboardService;
        private readonly ILookupService _lookupService;
        private readonly ILinkageService _linkageService;
        private Guid _encounterTypeId;
        private Client _client;
        private Encounter _encounter;


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

        public IReferralViewModel ReferralViewModel { get; set; }
        public ILinkedToCareViewModel LinkedToCareViewModel { get; set; }

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

        public LinkageViewModel(ILookupService lookupService, IDashboardService dashboardService, ILinkageService linkageService, ISettings settings)
        {
            _lookupService = lookupService;
            _dashboardService = dashboardService;
            _linkageService = linkageService;
            _settings = settings;

            ReferralViewModel=new ReferralViewModel();
            ReferralViewModel.ParentViewModel = this;
            LinkedToCareViewModel=new LinkedToCareViewModel();
            LinkedToCareViewModel.ParentViewModel = this;
        }

        public void Init(string formId, string encounterTypeId, string mode, string clientId, string encounterId)
        {

            // Load Client

                Client = _dashboardService.LoadClient(new Guid(clientId));

            if (null != Client)
            {
                var clientJson = JsonConvert.SerializeObject(Client);
                _settings.AddOrUpdateValue("client", clientJson);
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
                Encounter = _linkageService.StartEncounter(new Guid(formId), EncounterTypeId, Client.Id,AppProviderId,AppUserId,AppPracticeId,AppDeviceId);
            }
            else
            {
                //  Load Encounter
                _settings.AddOrUpdateValue("client.link.mode", "open");
                Encounter = _linkageService.OpenEncounter(new Guid(encounterId));
            }

            if (null == Encounter)
            {
                throw new ArgumentException("Encounter has not been Initialized");
            }
            //Store Encounter

            var encounterJson = JsonConvert.SerializeObject(Encounter);
            _settings.AddOrUpdateValue("client.encounter", encounterJson);

            var modes = _lookupService.GetCategoryItems("TraceMode", true, "[Select Mode]").ToList();
            _settings.AddOrUpdateValue("lookup.Mode", JsonConvert.SerializeObject(modes));

            var outcomes = _lookupService.GetCategoryItems("TraceOutcome", true, "[Select Outcome]").ToList();
            _settings.AddOrUpdateValue("lookup.Outcome", JsonConvert.SerializeObject(outcomes));

            var reasons = _lookupService.GetCategoryItems("ReasonNotContacted", true, "[Select Reason]").ToList();
            _settings.AddOrUpdateValue("lookup.ReasonNotContacted", JsonConvert.SerializeObject(reasons));

        }

        public override void ViewAppeared()
        {

            var clientJson = _settings.GetValue("client.dto", "");
            var clientEncounterJson = _settings.GetValue("client.encounter", "");
            var encounterTypeId = _settings.GetValue("encounterTypeId", "");

            if (null == Client && !string.IsNullOrWhiteSpace(clientJson))
            {
                Client = JsonConvert.DeserializeObject<Client>(clientJson);
            }

            if (EncounterTypeId.IsNullOrEmpty() && !string.IsNullOrWhiteSpace(encounterTypeId))
            {
                EncounterTypeId = new Guid(encounterTypeId);
            }

            if (null == Encounter && !string.IsNullOrWhiteSpace(clientEncounterJson))
            {
                Encounter = JsonConvert.DeserializeObject<Encounter>(clientEncounterJson);
            }

            if (null != Client && !Client.CanBeLinked())
            {
                LinkedToCareViewModel.ErrorSummary = "Client Cannot be linked to Care !";
                LinkedToCareViewModel.Validator.AddRule(
                    () => RuleResult.Assert(
                        Client.CanBeLinked(),
                        $"Client Cannot be linked to Care !"
                    )
                );

            }
        }



        private void LoadTraces()
        {

            var modesJson = _settings.GetValue("lookup.Mode", "");
            var outcomeJson = _settings.GetValue("lookup.Outcome", "");
            var reasonJson = _settings.GetValue("lookup.ReasonNotContacted", "");

            List<CategoryItem> modes=new List<CategoryItem>();
            if (!string.IsNullOrWhiteSpace(modesJson))
            {
                modes = JsonConvert.DeserializeObject<List<CategoryItem>>(modesJson);
            }

            List<CategoryItem> outcomes=new List<CategoryItem>();
            if ( !string.IsNullOrWhiteSpace(outcomeJson))
            {
                outcomes = JsonConvert.DeserializeObject<List<CategoryItem>>(outcomeJson);
            }


            List<CategoryItem> reasons=new List<CategoryItem>();
            if ( !string.IsNullOrWhiteSpace(reasonJson))
            {
                reasons = JsonConvert.DeserializeObject<List<CategoryItem>>(reasonJson);
            }


            if (null != Encounter)
            {
                 ReferralViewModel.Traces = ConvertToHIVTestWrapperClass(this, Encounter,modes,outcomes,reasons);

                var linkage = Encounter.ObsLinkages.ToList().FirstOrDefault();

                if (null != linkage)
                {
                    ReferralViewModel.ObsLinkage = linkage;
                    LinkedToCareViewModel.ObsLinkage = linkage;
                }
                else
                {
                    LinkedToCareViewModel.ErrorSummary = "Missing Referral information";
                    LinkedToCareViewModel.Validator.AddRule(
                        () => RuleResult.Assert(
                            false,
                            $"Missing Referral information !"
                        )
                    );
                }
            }
        }


        private static List<TraceTemplateWrap> ConvertToHIVTestWrapperClass(ILinkageViewModel clientDashboardViewModel, Encounter encounter,List<CategoryItem> modes, List<CategoryItem> outcomes,List<CategoryItem> reasons)
        {
           List<TraceTemplateWrap> list = new List<TraceTemplateWrap>();

            var testResults = encounter.ObsTraceResults.ToList();

            foreach (var r in testResults)
            {
                list.Add(new TraceTemplateWrap(clientDashboardViewModel.ReferralViewModel, new TraceTemplate(r, modes, outcomes,reasons)));
            }

            return list;
        }

        public Guid GetGuid(string key)
        {
            var guid = _settings.GetValue(key, "");

            if (string.IsNullOrWhiteSpace(guid))
                return Guid.Empty;

            return new Guid(guid);
        }

        public void GoBack()
        {
            ShowViewModel<DashboardViewModel>(new { id = Client.Id });
        }
    }
}
