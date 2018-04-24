using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.SharedKernel.Custom;

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
            PROVIDER_DETAILS=new PROVIDERDETAILS();
        }

        public static List<HIVTEST> Create()
        {
            return new List<HIVTEST>{new HIVTEST()};
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

        public override string ToString()
        {
            return $"{DATE}|{RESULT}|{TYPE}|{FACILITY} - {PROVIDER_DETAILS}";
        }

        public override bool Equals(object obj)
        {
            var hIVTEST = obj as HIVTEST;
            return hIVTEST != null &&
                   DATE.IsSameAs(hIVTEST.DATE) &&
                   RESULT.IsSameAs(hIVTEST.RESULT) &&
                   TYPE.IsSameAs(hIVTEST.TYPE) &&
                   FACILITY.IsSameAs(hIVTEST.FACILITY) &&
                   STRATEGY.IsSameAs(hIVTEST.STRATEGY);
        }

        public override int GetHashCode()
        {
            var hashCode = -1794862602;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DATE);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RESULT);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TYPE);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FACILITY);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(STRATEGY);
            return hashCode;
        }
    }
}