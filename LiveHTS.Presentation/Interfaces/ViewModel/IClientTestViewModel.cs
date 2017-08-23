using System.Collections.Generic;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel.Widget;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientHIVTestViewModel
    {
        Client Client { get; set; }

        string FirstTestName { get; set; }
        List<Test> FirstTests { get; set; }

        void SaveTest(Test test);
        void DeleteTest(Test test);
        void RefreshTest();

        //        List<CategoryItem> FirstTestResults { get; set; }
        //        CategoryItem SelectedFirstTestResult { get; set; }
        //
        //        string SecondTestName { get; set; }
        //        List<Test> SecondTests { get; set; }
        //        List<CategoryItem> SecondTestResults { get; set; }
        //        CategoryItem SelectedSecondTestResult { get; set; }
        //
        //        List<CategoryItem> FinalTestResults { get; set; }
        //        CategoryItem SelectedFinalTestResult { get; set; }
    }
}