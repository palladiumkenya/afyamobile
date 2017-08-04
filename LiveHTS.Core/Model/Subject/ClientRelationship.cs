﻿using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
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
        [Ignore]
        public  Person Person { get; set; }

        public ClientRelationship()
        {
            Id = LiveGuid.NewGuid();
        }

        private ClientRelationship(string relationshipTypeId, Guid relatedClientId, bool preferred, Guid clientId):this()
        {
            RelationshipTypeId = relationshipTypeId;
            RelatedClientId = relatedClientId;
            Preferred = preferred;
            ClientId = clientId;
        }

        public static ClientRelationship Create(string relationshipTypeId, Guid relatedClientId, bool preferred, Guid clientId)
        {
            return new ClientRelationship(relationshipTypeId, relatedClientId, preferred,clientId);
        }

        public override string ToString()
        {
            string person = null != Person ? Person.ToString() : string.Empty;
            return $"{RelationshipTypeId}|{RelatedClientId} - {person}";
        }
    }
}