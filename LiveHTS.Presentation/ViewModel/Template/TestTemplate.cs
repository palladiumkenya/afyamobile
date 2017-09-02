using System;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class TestTemplate : ITestTemplate
    {
        public ObsTestResult TestResult { get; set; }
        public Guid Id { get; set; }
        public string ResultDisplay { get; set; }
        public string KitDisplay { get; set; }
        public bool ShowKitOther { get; set; }
        public string KitOther { get; set; }
        public string LotNumber { get; set; }
        public DateTime Expiry { get; set; }

        public TestTemplate(ObsTestResult test)
        {
            TestResult = test;
            Id = test.Id;
            ResultDisplay = test.ResultDisplay;
            KitDisplay = test.KitDisplay;
            KitOther = test.KitOther;
            ShowKitOther = !string.IsNullOrWhiteSpace(KitOther);
            LotNumber = test.LotNumber;
            Expiry = test.Expiry;
        }
    }
}