using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IHIVTestTemplate
    {
        ObsTestResult TestResult { get; }
        IHIVTestTemplateWrap HIVTestTemplateWrap { get; set; }
        Guid Id { get; set; }
        string TestName { get; set; }
        int Attempt { get; set; }
        Guid Kit { get; set; }
        bool ShowKitOther { get; set; }
        string KitOther { get; set; }
        string LotNumber { get; set; }
        DateTime Expiry { get; set; }
        Guid Result { get; set; }
        Guid EncounterId { get; set; }

        CategoryItem SelectedKit { get; set; }
        CategoryItem SelectedResult { get; set; }

        List<CategoryItem> Kits { get; set; }
        List<CategoryItem> Results { get; set; }
        bool CanSave();
        bool CanDelete();
    }
}