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
        string TestName { get; set; }
        string TestNameResult { get; set; }
        List<ObsTestResult> Tests { get; set; }
        CategoryItem SelectedTestResult { get; set; }
        List<CategoryItem> TestResults { get; set; }
        IMvxCommand AddTestCommand { get; }
        IMvxCommand CloseTestCommand { get; }
        Action AddTestCommandAction { get; set; }
        Action CloseTestCommandAction { get; set; }
    }
}