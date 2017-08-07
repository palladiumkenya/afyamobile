using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientDashboardViewModel
    {
        Client Client { get; set; }        
        Client SeletctedRelationShip { get; set; }
        List<RelationshipTemplateWrap> Relationships { get; set; }
        Module Module { get; set; }
        IEnumerable<Form> Forms { get; set; }

        IMvxCommand ManageRegistrationCommand { get; }
        IMvxCommand AddRelationShipCommand { get; }     

        bool IsBusy { get; set; }

        void ShowRegistry();

        void RemoveRelationship(RelationshipTemplate relationshipTemplate);
        void StartEncounter(FormTemplate encounterTemplate);
        void ResumeEncounter(EncounterTemplate encounterTemplate);
        void ReviewEncounter(EncounterTemplate encounterTemplate);
        void DiscardEncounter(EncounterTemplate encounterTemplate);
    }
}