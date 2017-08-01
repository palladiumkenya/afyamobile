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
        public IEnumerable<ClientIdentifier> Identifiers { get; set; }
        [Ignore]
        public IEnumerable<ClientRelationship> Relationships { get; set; }
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

        public override string ToString()
        {
            return $"{Person} ,{Person.Gender}";
        }
    }
}