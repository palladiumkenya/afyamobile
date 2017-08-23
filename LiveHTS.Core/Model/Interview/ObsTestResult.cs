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
        public string KitOther { get; set; }
        public string LotNumber { get; set; }
        public DateTime Expiry { get; set; }
        [Indexed]
        public Guid Result { get; set; }

        public ObsTestResult()
        {
            Id = LiveGuid.NewGuid();
        }

        private ObsTestResult(string testName, int attempt, Guid kit, string kitOther, string lotNumber, DateTime expiry, Guid result):this()
        {
            TestName = testName;
            Attempt = attempt;
            Kit = kit;
            KitOther = kitOther;
            LotNumber = lotNumber;
            Expiry = expiry;
            Result = result;
        }

        public static ObsTestResult Create(Guid id,string testName, int attempt, Guid kit, string kitOther, string lotNumber, DateTime expiry, Guid result)
        {
            var obs=new ObsTestResult(testName, attempt, kit, kitOther, lotNumber, expiry, result);
            obs.Id = id;
            return obs;
        }
        public static ObsTestResult Create(string testName, int attempt, Guid kit, string kitOther, string lotNumber, DateTime expiry, Guid result)
        {
            return new ObsTestResult(testName, attempt, kit, kitOther, lotNumber, expiry, result);
        }
       
    }
}