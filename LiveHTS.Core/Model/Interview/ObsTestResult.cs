using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsTestResult : Entity<Guid>
    {
        public string TestName { get; set; }
        public int Attempt {get; set; }
        [Indexed]
        public Guid Kit { get; set; }
        [Ignore]
        public string KitDisplay { get; set; }
        public string KitOther { get; set; }
        public string LotNumber { get; set; }
        public DateTime Expiry { get; set; }
        [Indexed]
        public Guid Result { get; set; }
        public string ResultCode { get; set; }

        [Ignore]
        public string ResultDisplay { get; set; }
        public bool IsValid { get; set; }
        [Indexed]
        public Guid EncounterId { get; set; }

        public ObsTestResult()
        {
            Id = LiveGuid.NewGuid();
        }

        private ObsTestResult(string testName, int attempt, Guid kit, string kitOther, string lotNumber, DateTime expiry, Guid result, Guid encounterId, string resultCode="") :this()
        {
            TestName = testName;
            Attempt = attempt;
            Kit = kit;
            KitOther = kitOther;
            LotNumber = lotNumber;
            Expiry = expiry;
            Result = result;
            EncounterId = encounterId;
            ResultCode = resultCode;
        }

        private ObsTestResult(string testName, int attempt, Guid encounterId):this()
        {
            TestName = testName;
            Attempt = attempt;
            EncounterId = encounterId;
            Expiry = DateTime.Today.AddYears(1);
        }

        public static ObsTestResult Create(Guid id,string testName, int attempt, Guid kit, string kitOther, string lotNumber, DateTime expiry, Guid result, Guid encounterId, string resultCode = "")
        {
            var obs=new ObsTestResult(testName, attempt, kit, kitOther, lotNumber, expiry, result, encounterId, resultCode);
            obs.Id = id;
            return obs;
        }
        public static ObsTestResult Create(string testName, int attempt, Guid kit, string kitOther, string lotNumber, DateTime expiry, Guid result, Guid encounterId, string resultCode = "")
        {
            return new ObsTestResult(testName, attempt, kit, kitOther, lotNumber, expiry, result, encounterId, resultCode);
        }
        public static ObsTestResult CreateNew(string testName, int attempt, Guid encounterId)
        {
            return new ObsTestResult(testName, attempt, encounterId);
        }

    }
}