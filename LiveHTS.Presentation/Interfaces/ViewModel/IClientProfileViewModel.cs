using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.DTO;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientProfileViewModel : IStepViewModel
    {
        bool IsRelation { get; set; }
        string IndexClientName { get; set; }
        IEnumerable<RelationshipType> RelationshipTypes { get; set; }
        RelationshipType SelectedRelationshipType { get; set; }
        IndexClientDTO IndexClientDTO { get; set; }
        ClientProfileDTO Profile { get; set; }
        string ClientInfo { get; set; }
        IEnumerable<MaritalStatus> MaritalStatus { get; set; }
        IEnumerable<KeyPop> KeyPops { get; set; }
        MaritalStatus SelectedMaritalStatus { get; set; }
        KeyPop SelectedKeyPop { get; set; }
        string IsOtherKeyPop { get; set; }
        string OtherKeyPop { get; set; }

        List<CategoryItem> Educations { get; set; }
        CategoryItem SelectedEducation { get; set; }
        bool AllowCompletion { get; set; }
        List<CategoryItem> Completions { get; set; }
        CategoryItem SelectedCompletion { get; set; }

        string ClientId { get; set; }
    }
}