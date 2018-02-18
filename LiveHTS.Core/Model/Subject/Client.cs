using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class Client:Entity<Guid>, IProfile
    {
        [Ignore]
        public IEnumerable<ClientIdentifier> Identifiers { get; set; } = new List<ClientIdentifier>();
        [Ignore]
        public IEnumerable<ClientRelationship> Relationships { get; set; } = new List<ClientRelationship>();
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string OtherKeyPop { get; set; }
        public bool IsFamilyMember  { get; set; }
        public bool IsPartner { get; set; }
        [Indexed]
        public Guid PracticeId { get; set; }
        [Indexed]
        public Guid PersonId { get; set; }
        [Ignore]
        public Person Person { get; set; }
        [Indexed]
        public Guid? CohortId { get; set; }
        public bool EncountersDownloaded { get; set; }
        public bool Downloaded { get; set; }
        public bool? PreventEnroll { get; set; }

        [Ignore]
        public IEnumerable<ClientRelationship> MyRelationships { get; set; }=new List<ClientRelationship>();
        [Ignore]
        public IEnumerable<ClientRelationship> RelatedToMe { get; set; } = new List<ClientRelationship>();
        public Client()
        {
            Id = LiveGuid.NewGuid();
        }

        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId):this()
        {
            MaritalStatus = maritalStatus;
            KeyPop = keyPop;
            OtherKeyPop = otherKeyPop;
            PracticeId = practiceId;
        }

        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Guid personId)
            :this(maritalStatus, keyPop, otherKeyPop,practiceId)
        {
            PersonId = personId;
        }
        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Person person)
            : this(maritalStatus, keyPop, otherKeyPop, practiceId)
        {
            Person = person;
        }

        public static Client Create(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Person person)
        {
            return new Client(maritalStatus, keyPop, otherKeyPop,practiceId,person);
        }
        public static Client CreateFromPerson(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId,  Guid personId)
        {
            return new Client(maritalStatus, keyPop, otherKeyPop, practiceId, personId);
        }

        public bool DisableHts()
        {
            return null != PreventEnroll && PreventEnroll.Value;
        }
        public override string ToString()
        {
            return $"{Person} ,{Person.Gender}";
        }

     
    }
}