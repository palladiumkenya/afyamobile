using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsFinalTestResult : Entity<Guid>
    {
        [Indexed]
        public Guid? FirstTestResult { get; set; }
        [Indexed]
        public Guid? SecondTestResult { get; set; }
        [Indexed]
        public Guid? EndResult { get; set; }
        [Indexed]
        public Guid EncounterId { get; set; }


        public ObsFinalTestResult()
        {
            Id = LiveGuid.NewGuid();
        }

        private ObsFinalTestResult(Guid id, Guid? firstTestResult, Guid? secondTestResult, Guid? endResult, Guid encounterId) 
        {
            Id = id;
            FirstTestResult = firstTestResult;
            SecondTestResult = secondTestResult;
            EndResult = endResult;
            EncounterId = encounterId;
        }

        private ObsFinalTestResult(Guid? firstTestResult, Guid? secondTestResult, Guid? endResult, Guid encounterId) : this(LiveGuid.NewGuid(),firstTestResult,secondTestResult,endResult,encounterId)
        {
            
        }
        public static ObsFinalTestResult Create(Guid id, Guid? firstTestResult, Guid? secondTestResult, Guid? endResult, Guid encounterId)
        {
            return new ObsFinalTestResult(id, firstTestResult, secondTestResult, endResult,encounterId);
        }
        public static ObsFinalTestResult Create(Guid? firstTestResult, Guid? secondTestResult, Guid? endResult, Guid encounterId) 
        {
            return new ObsFinalTestResult(firstTestResult, secondTestResult, endResult,encounterId);
        }
    }
}