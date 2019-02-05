using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class Form:Entity<Guid>
    {
        public string Name { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public decimal Rank { get; set; }
        [Indexed]
        public Guid ModuleId { get; set; }
        [Ignore]
        public List<Question> Questions { get; set; }=new List<Question>();

        [Ignore]
        public List<Program> Programs { get; set; } = new List<Program>();
        [Ignore]
        public Guid DefaultEncounterTypeId { get; set; }

        [Ignore]
        public List<Encounter> ClientEncounters { get; set; }=new List<Encounter>();

        [Ignore]
        public List<Encounter> KeyClientEncounters { get; set; } = new List<Encounter>();

        [Ignore] public List<ClientState> ClientStates { get; set; } = new List<ClientState>();
        [Ignore] public Guid? IndexClientId { get; set; }

        [Ignore]
        public bool ConsentRequired
        {
            get { return CheckConsentRequired(); }
        }

        [Ignore]
        public bool HasConsent
        {
            get {return CheckClientState(); }
        }
        [Ignore]
        public bool Block { get; set; }

        [Ignore]
        public bool ClientDownloaded { get; set; }

        [Ignore]
        public bool IsRepeat
        {
            get
            {
                if (ClientStates.Count > 0)
                {
                    //downloaded

                    if (ClientDownloaded)
                    {
                        return ClientState.IsInState(ClientStates, LiveState.HtsTested);
                    }
                    if (ClientState.IsInState(ClientStates, LiveState.HtsSmartCardEnrolled))
                    {
                        return ClientState.IsInState(ClientStates, LiveState.HtsTested);
                    }
                }

                return false;
                
            }
        }
        [Ignore]
        public bool CanStart
        {
            get { return CheckCanStart(); }
        }

       

        private bool CheckConsentRequired()
        {
            //HIV Test Form
            //HTS Linkage Form
            /*
             Id	Description
B25EC112-852F-11E7-BB31-BE2E44B06B34	HTS Linkage Form
B25EC568-852F-11E7-BB31-BE2E44B06B34	HIV Test Form
B25EBCDA-852F-11E7-BB31-BE2E44B06B34	HTS Lab Form

Id	Description	Display	ModuleId	Name
B25EC112-852F-11E7-BB31-BE2E45B06B35	Member Screening	Member Screening	B260C688-852F-11E7-BB31-BE2E45B06B55	Member Screening
B25EC112-852F-11E7-BB31-BE2E45B06B36	Member Tracing	Member Tracing	B260C688-852F-11E7-BB31-BE2E45B06B55	Member Tracing
B25EC112-852F-11E7-BB31-BE2E46B06B37	Partner Screening	Partner Screening	B260C688-852F-11E7-BB31-BE2E46B06B66	Partner Screening
B25EC112-852F-11E7-BB31-BE2E46B06B38	Partner Tracing	Partner Tracing	B260C688-852F-11E7-BB31-BE2E46B06B66	Partner Tracing
             */
            return Name.ToLower() == "HIV Test Form".ToLower()|| Name.ToLower() == "HTS Linkage Form".ToLower() || Name.ToLower() == "Member Tracing".ToLower() || Name.ToLower() == "Partner Tracing".ToLower();
        }

        private bool CheckConsent()
        {
            if (Name.ToLower() == "HIV Test Form".ToLower())
            {
                var obsEncounter =KeyClientEncounters.FirstOrDefault(x => x.FormId == new Guid("B25EBCDA-852F-11E7-BB31-BE2E44B06B34"));


                if (CheckConsentRequired() && null != obsEncounter)
                {
                    var obs = obsEncounter.Obses.ToList();

                    if (null != obs && obs.Count > 0)
                    {
                        // consent
                        var consent = obs.FirstOrDefault(x =>
                            x.QuestionId == new Guid("B2603DC6-852F-11E7-BB31-BE2E44B06B34"));

                        if (null != consent && !consent.ValueCoded.IsNullOrEmpty())
                        {
                            return consent.ValueCoded == new Guid("B25ECCD4-852F-11E7-BB31-BE2E44B06B34");
                        }
                    }

                }
                return false;
            }

            if (Name.ToLower() == "HTS Linkage Form".ToLower())
            {
                var obsTestEncounter = KeyClientEncounters.FirstOrDefault(x => x.FormId == new Guid("B25EC568-852F-11E7-BB31-BE2E44B06B34"));

                if (CheckConsentRequired() && null != obsTestEncounter)
                {
                    var finalTestResults = obsTestEncounter.ObsFinalTestResults.ToList();

                    if (null != finalTestResults && finalTestResults.Count > 0)
                    {
                        // consent
                        var finalTestResult = finalTestResults.FirstOrDefault(x => null!= x.FinalResult  && x.FinalResult == new Guid("b25efd8a-852f-11e7-bb31-be2e44b06b34"));
                        return null != finalTestResult;
                    }
                }

                return false;
            }

            if (Name.ToLower() == "Member Tracing".ToLower())
            {
                var screeningEncounter = KeyClientEncounters.FirstOrDefault(x => x.FormId == new Guid("B25EC112-852F-11E7-BB31-BE2E45B06B35"));


                if (CheckConsentRequired() && null != screeningEncounter)
                {
                    var memberScreenings = screeningEncounter.ObsMemberScreenings.ToList();

                    if (null != memberScreenings && memberScreenings.Count > 0)
                    {
                        // consent
                        var memberScreening = memberScreenings.FirstOrDefault(x => null != x.Eligibility && x.Eligibility == new Guid("B25ECCD4-852F-11E7-BB31-BE2E44B06B34"));
                        return null != memberScreening;
                    }
                }
                return false;
            }

            if (Name.ToLower() == "Partner Tracing".ToLower())
            {
                var screeningEncounter = KeyClientEncounters.FirstOrDefault(x => x.FormId == new Guid("B25EC112-852F-11E7-BB31-BE2E46B06B37"));


                if (CheckConsentRequired() && null != screeningEncounter)
                {
                    var partnerScreenings = screeningEncounter.ObsPartnerScreenings.ToList();

                    if (null != partnerScreenings && partnerScreenings.Count > 0)
                    {
                        // consent
                        var partnerScreening = partnerScreenings.FirstOrDefault(x => null != x.Eligibility && x.Eligibility == new Guid("B25ECCD4-852F-11E7-BB31-BE2E44B06B34"));
                        return null != partnerScreening;
                    }
                }
                return false;
            }

            return false;
        }

        private bool CheckClientState()
        {
            //HIV Test Form 
            if (Id == new Guid("b25ec568-852f-11e7-bb31-be2e44b06b34"))
            {
               return ClientState.IsInState(ClientStates, LiveState.HtsConsented);
            }

            //HTS Linkage Form 
            if (Id == new Guid("b25ec112-852f-11e7-bb31-be2e44b06b34"))
            {
                return ClientState.IsInAnyState(ClientStates, LiveState.HtsTestedPos,LiveState.HtsTestedInc, LiveState.HtsCanBeReferred);
            }

            //Member Tracing
            if (Id == new Guid("b25ec112-852f-11e7-bb31-be2e45b06b36") && 
                null != IndexClientId &&!IndexClientId.Value.IsNullOrEmpty())
            {
                return ClientState.IsInState(ClientStates,IndexClientId.Value, LiveState.FamilyEligibileYes);
            }

            //Partner Tracing
            if (Id == new Guid("b25ec112-852f-11e7-bb31-be2e46b06b38") &&
            null != IndexClientId && !IndexClientId.Value.IsNullOrEmpty())
            {
                return ClientState.IsInState(ClientStates, IndexClientId.Value,LiveState.PartnerEligibileYes);
            }
            return false;
        }

        private bool CheckCanStart()
        {
            //HTS Linkage Form 
            if (Id == new Guid("b25ec112-852f-11e7-bb31-be2e44b06b34"))
            {
                return ClientState.IsInAnyState(ClientStates, LiveState.HtsTestedPos, LiveState.HtsTestedInc);
            }
            return false;
        }
        public Form()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Display}";
        }
    }
}