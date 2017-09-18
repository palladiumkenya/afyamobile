using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ITestingViewModel
    {

        Guid AppUserId { get; }
        Guid AppProviderId { get;  }
        Guid AppPracticeId { get;  }
        Guid AppDeviceId { get;  }

        string ErrorSummary { get; set; }
        ITestEpisodeViewModel FirstTestEpisodeViewModel { get; set; }
        ITestEpisodeViewModel SecondTestEpisodeViewModel { get; set; }

        EncounterType EncounterType { get; set; }
        Client Client { get; set; }
        Encounter Encounter { get; set; }
        ObsFinalTestResult ObsFinalTestResult { get; set; }

        bool EnableFirstResult { get; set; }
        Guid FirstResult { get; set; }
        CategoryItem SelectedFirstTestResult { get; set; }
        List<CategoryItem> FirstTestResults { get; set; }

        bool EnableSecondResult { get; set; }
        Guid SecondResult { get; set; }
        CategoryItem SelectedSecondTestResult { get; set; }
        List<CategoryItem> SecondTestResults { get; set; }

        bool HasFinalResult { get;  }
        bool EnableFinalResult { get; set; }
        Guid FinalResult { get; set; }
        CategoryItem SelectedFinalTestResult { get; set; }
        List<CategoryItem> FinalTestResults { get; set; }

        Guid ResultGiven { get; set; }
        CategoryItem SelectedResultGiven { get; set; }
        List<CategoryItem> ResultGivenOptions { get; set; }

        Guid CoupleDiscordant { get; set; }
        CategoryItem SelectedCoupleDiscordant { get; set; }
        List<CategoryItem> CoupleDiscordantOptions { get; set; }

        Guid SelfTestOption { get; set; }
        CategoryItem SelectedSelfTest { get; set; }
        List<CategoryItem> SelfTestOptions { get; set; }

        List<CategoryItem> Kits { get; set; }
        
        IMvxCommand SaveTestingCommand { get; }

        void SaveTest(ObsTestResult test);
        void DeleteTest(ObsTestResult test);
        void Referesh(Guid encounterId);
        bool Validate();
    }
}