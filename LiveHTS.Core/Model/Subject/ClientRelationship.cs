using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using Newtonsoft.Json;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class ClientRelationship : Entity<Guid>
    {
        [Indexed]
        public string RelationshipTypeId { get; set; }
        [Indexed]
        public Guid RelatedClientId { get; set; }
        public bool Preferred { get; set; }
        [Indexed]
        public Guid ClientId { get; set; }
        public bool? IsIndex { get; set; }
        
        
        
        [Ignore]
        [JsonIgnore]
        public  Person Person { get; set; }

        public ClientRelationship()
        {
            Id = LiveGuid.NewGuid();
        }

        private ClientRelationship(string relationshipTypeId, Guid relatedClientId, bool preferred, Guid clientId, bool? isIndex) : this()
        {
            RelationshipTypeId = relationshipTypeId;
            RelatedClientId = relatedClientId;
            Preferred = preferred;
            ClientId = clientId;
            IsIndex = isIndex;
        }
        
        public static ClientRelationship Create(string relationshipTypeId, Guid relatedClientId, bool preferred, Guid clientId, bool? isIndex=null)
        {
            return new ClientRelationship(relationshipTypeId, relatedClientId, preferred, clientId,isIndex);
        }

        public bool IsIndexRelation()
        {
            return null != IsIndex && IsIndex.Value;
        }
        public bool IsPatner()
        {
            return (RelationshipTypeId.ToLower().Trim() == "Partner".ToLower().Trim() ||
                    RelationshipTypeId == "Cowife".ToLower().Trim() || RelationshipTypeId == "Spouse".ToLower().Trim());
        }
        public bool IsFamily()
        {
            return !IsPatner();
        }
        public bool IsFamilyRelation()
        {
            return !IsPatner() && IsIndexRelation();
        }
        public bool IsPartnerRelation()
        {
            return IsPatner() && IsIndexRelation();
        }

        public override string ToString()
        {
            string person = null != Person ? Person.ToString() : string.Empty;
            return $"{RelationshipTypeId}|{RelatedClientId} - {person}";
        }
    }
}