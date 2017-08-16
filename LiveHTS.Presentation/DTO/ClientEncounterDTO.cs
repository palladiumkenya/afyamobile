using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;

namespace LiveHTS.Presentation.DTO
{
    public class ClientEncounterDTO
    {
        public Guid ClientId { get; set; }
        public Guid FormId { get; set; }
        public string FormDisplay { get; set; }
        public Guid EncounterTypeId { get; set; }

        public ClientEncounterDTO()
        {
        }

        private ClientEncounterDTO(Guid clientId, Guid formId, string formDisplay, Guid encounterTypeId)
        {
            ClientId = clientId;
            FormId = formId;
            FormDisplay = formDisplay;
            EncounterTypeId = encounterTypeId;
        }

        public static ClientEncounterDTO Create(Guid clientId, EncounterTemplate encounterTemplate)
        {
            return new ClientEncounterDTO(
                clientId,
                encounterTemplate.FormId,
                encounterTemplate.FormDisplay,
                encounterTemplate.EncounterTypeId
                );
        }

        public static ClientEncounterDTO Create(Guid clientId, FormTemplate formTemplate)
        {
            return new ClientEncounterDTO(
                clientId,
                formTemplate.Id,
                formTemplate.Display,
                formTemplate.DefaultEncounterTypeId
            );
        }
    }
}