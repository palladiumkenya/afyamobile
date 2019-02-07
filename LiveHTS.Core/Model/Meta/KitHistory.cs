using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Meta
{
    public class KitHistory
    {
        public Guid KitId { get; set; }
        public string Batch { get; set; }
        public DateTime Expiry { get; set; }

        public KitHistory()
        {
        }

        public KitHistory(Guid kitId, string batch, DateTime expiry):this()
        {
            KitId = kitId;
            Batch = batch;
            Expiry = expiry;
        }

        public static KitHistory Create(ObsTestResult testResult)
        {
            if (null != testResult)
                return new KitHistory(testResult.Kit, testResult.LotNumber, testResult.Expiry);
            
            return new KitHistory();
        }

        public static List<KitHistory> Create(List<ObsTestResult> testResults)
        {
            var histories = new List<KitHistory>();

            if (null != testResults)
            {
                foreach (var result in testResults)
                {
                    histories.Add(Create(result));
                }
            }

            return histories;
        }
    }
}
