﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IFirstHIVTestViewModel:IMvxViewModel
    {
        IHIVTestViewModel Parent { get; set; }
        string FirstTestName { get; set; }
        ObservableCollection<HIVTestTemplateWrap> FirstTests { get; set; }
        CategoryItem SelectedFirstTestResult { get; set; }
        List<CategoryItem> FirstTestResults { get; set; }
        IMvxCommand AddFirstTestCommand { get; }
    }
}