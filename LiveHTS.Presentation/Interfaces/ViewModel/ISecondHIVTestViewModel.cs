using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ISecondHIVTestViewModel : IMvxViewModel
    {
        IHIVTestViewModel Parent { get; set; }
        string SecondTestName { get; set; }
        ObservableCollection<HIVTestTemplateWrap> SecondTests { get; set; }
        CategoryItem SelectedSecondTestResult { get; set; }
        List<CategoryItem> SecondTestResults { get; set; }
        IMvxCommand AddSecondTestCommand { get; }
    }
}