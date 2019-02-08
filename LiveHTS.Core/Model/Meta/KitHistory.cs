using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Meta
{
    public class KitHistory:Entity<Guid>
    {
        public string Batch { get; set; }
        public DateTime Expiry { get; set; }
        public DateTime? Created { get; set; }

        public KitHistory()
        {
        }

        public KitHistory(Guid kitId, string batch, DateTime expiry, DateTime? created)
        {
            Id = kitId;
            Batch = batch;
            Expiry = expiry;
            Created = created.IsNullOrEmpty()?DateTime.Now:created;
        }

        public static KitHistory Create(ObsTestResult testResult)
        {
            if (null != testResult)
                return new KitHistory(testResult.Kit, testResult.LotNumber, testResult.Expiry,testResult.Created);
            
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
