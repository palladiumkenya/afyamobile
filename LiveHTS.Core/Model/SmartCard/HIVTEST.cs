using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Model.SmartCard
{
    public class HIVTEST:IEqualityComparer<HIVTEST>
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

        public bool Equals(HIVTEST b1, HIVTEST b2)
        {
            if (b2 == null && b1 == null)
                return true;
            if (b1 == null | b2 == null)
                return false;
            if (
                b1.DATE.IsSameAs(b2.DATE) &&
                b1.RESULT.IsSameAs(b2.RESULT) &&
                b1.FACILITY.IsSameAs(b2.FACILITY)
                )
                return true;
            else
                return false;
        }

        public int GetHashCode(HIVTEST obj)
        {
            int hCode = obj.DATE.GetHashCode() + obj.RESULT.GetHashCode() + obj.TYPE.GetHashCode() +
                        obj.FACILITY.GetHashCode();

            return hCode.GetHashCode();
        }
    }
}