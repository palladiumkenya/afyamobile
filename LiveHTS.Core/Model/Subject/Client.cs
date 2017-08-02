using System;
using System.Collections.Generic;
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
        [Indexed]
        public Guid PracticeId { get; set; }
        [Indexed]
        public Guid PersonId { get; set; }
        [Ignore]
        public Person Person { get; set; }

        public Client()
        {
            Id = LiveGuid.NewGuid();
        }

        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Guid personId)
        {
            MaritalStatus = maritalStatus;
            KeyPop = keyPop;
            OtherKeyPop = otherKeyPop;
            PracticeId = practiceId;
            PersonId = personId;
        }
        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Person person)
        {
            MaritalStatus = maritalStatus;
            KeyPop = keyPop;
            OtherKeyPop = otherKeyPop;
            PracticeId = practiceId;
            PersonId = person.Id;
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

        public override string ToString()
        {
            return $"{Person} ,{Person.Gender}";
        }
    }
}