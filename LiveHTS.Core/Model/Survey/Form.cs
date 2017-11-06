using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Interview;
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

        [Ignore]
        public bool ConsentRequired
        {
            get { return CheckConsentRequired(); }
        }

        [Ignore]
        public bool HasConsent
        {
            get {return  CheckConsent(); }
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
             */
            return Name.ToLower() == "HIV Test Form".ToLower()|| Name.ToLower() == "HTS Linkage Form".ToLower();
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