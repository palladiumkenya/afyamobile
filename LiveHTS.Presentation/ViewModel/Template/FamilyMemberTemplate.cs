using System;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class FamilyMemberTemplate : IFamilyMemberTemplate
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string RelationshipTypeId { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid RelatedClientId { get; set; }
        public Guid ClientId { get; set; }
        public bool IsIndex { get; set; }

        public FamilyMemberTemplate(ClientRelationship relationship)
        {
            Id = relationship.Id;
            FullName = relationship.Person.FullName;
            Gender = relationship.Person.Gender;
            RelationshipTypeId = relationship.RelationshipTypeId;
            BirthDate = relationship.Person.BirthDate;
            ClientId = relationship.ClientId;
            RelatedClientId = relationship.RelatedClientId;
            IsIndex = null != relationship.IsIndex && relationship.IsIndex.Value;
        }
    }
}