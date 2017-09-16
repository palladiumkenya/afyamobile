using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Model
{
    public class SyncClientDTO
    {
        public Guid Id { get; set; }
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string OtherKeyPop { get; set; }
        public Guid? PracticeId { get; set; }
        public string PracticeCode { get; set; }
        public Person Person { get; set; }
        public List<ClientIdentifier> Identifiers { get; set; } = new List<ClientIdentifier>();
        public List<ClientRelationship> Relationships { get; set; } = new List<ClientRelationship>();

        public SyncClientDTO(Client client)
        {
            Id = client.Id;
            MaritalStatus = client.MaritalStatus;
            KeyPop = client.KeyPop;
            OtherKeyPop = client.OtherKeyPop;
            PracticeId = client.PracticeId;
            Person = client.Person;
            Identifiers = client.Identifiers.ToList();
            Relationships = client.Relationships.ToList();
        }
    }
}