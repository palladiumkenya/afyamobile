using System;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Model.SmartCard
{
    public class HIVTEST
    {
        public string DATE { get; set; }
        public string RESULT { get; set; }
        public string TYPE { get; set; }
        public string FACILITY { get; set; }
        public string STRATEGY { get; set; }
        public PROVIDERDETAILS PROVIDER_DETAILS { get; set; }

        public HIVTEST()
        {
        }

        private HIVTEST(string date, string result, string type, string facility)
        {
            DATE = date;
            RESULT = result;
            TYPE = type;
            FACILITY = facility;
            STRATEGY = "HP";
            PROVIDER_DETAILS = new PROVIDERDETAILS("Admin","1");
        }

        public static HIVTEST Create(DateTime encounterDate,ObsFinalTestResult finalTestResult,string code)
        {
           return new HIVTEST(encounterDate.ToString("yyyyMMdd"),GetResult(finalTestResult.FinalResult), "SCREENING", code);
        }

        private static string GetResult(Guid? id)
        {
            if (!id.HasValue)
                return "";

            if (id == new Guid("b25efd8a-852f-11e7-bb31-be2e44b06b34"))
                return "POSITIVE";
            if (id == new Guid("b25efb78-852f-11e7-bb31-be2e44b06b34"))
                return "NEGATIVE";
            if (id == new Guid("b25f017c-852f-11e7-bb31-be2e44b06b34"))
                return "INCONCLUSIVE";

            return "";
        }
    }
}