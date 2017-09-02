using System;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface ITestTemplate
    {
        ObsTestResult TestResult { get; set; }
        Guid Id { get; set; }
        string ResultDisplay { get; set; }
        string KitDisplay { get; set; }
        bool ShowKitOther { get; set; }
        string KitOther { get; set; }
        string LotNumber { get; set; }
        DateTime Expiry { get; set; }
    }
}