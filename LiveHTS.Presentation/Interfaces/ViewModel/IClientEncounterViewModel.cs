using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientEncounterViewModel
    {
        Guid UserId { get; }
        string UserName { get; }
        Guid ProviderId { get; }
        string ProviderName { get; }

        ClientDTO ClientDTO { get; set; }
        ClientEncounterDTO ClientEncounterDTO { get; set; }
        Form Form { get; set; }

        List<QuestionTemplateWrap> Questions { get; set; }
        Encounter Encounter { get; set; }

        event EventHandler<ConceptChangedEvent> ConceptChanged;

        IMvxCommand SaveChangesCommand { get; }
    }
}