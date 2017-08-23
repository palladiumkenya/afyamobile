using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientEncounterViewModel
    {
        bool AtTheEnd { get; set; }
        bool IsLoading { get; set; }
        Guid UserId { get; }
        string UserName { get; }
        Guid ProviderId { get; }
        string ProviderName { get; }
        string FormError { get; set; }
        string FormStatus { get; set; }

        ClientDTO ClientDTO { get; set; }
        ClientEncounterDTO ClientEncounterDTO { get; set; }

        Form Form { get; set; }
        ObservableCollection<QuestionTemplateWrap> Questions { get; set; }

        Encounter Encounter { get; set; }
        Manifest Manifest { get; set; }
        
        IMvxCommand SaveChangesCommand { get; }

        void LoadView();
        bool ValidateResponse(QuestionTemplate questionTemplate);
        void AllowNextQuestion(QuestionTemplate questionTemplate);
    }
}