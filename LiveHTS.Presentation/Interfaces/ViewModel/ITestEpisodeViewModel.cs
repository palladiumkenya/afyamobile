using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ITestEpisodeViewModel:IMvxViewModel
    {
        ITestingViewModel Parent { get; set; }
        bool EnableResult { get; set; }

        string TestName { get; set; }
        List<ObsTestResult> Tests { get; set; }
        ObsTestResult Test { get; set; }
        List<TestTemplateWrap> HivTests { get; set; }
        
        IMvxCommand AddTestCommand { get; }
        IMvxCommand CloseTestCommand { get; }
        Action AddTestCommandAction { get; set; }
        Action EditTestCommandAction { get; set; }
        Action CloseTestCommandAction { get; set; }

        void DeleteTest(ObsTestResult testResult);
        void EditTest(ObsTestResult testResult);
    }
}