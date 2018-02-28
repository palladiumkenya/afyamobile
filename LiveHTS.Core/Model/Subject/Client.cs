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
        public bool? AlreadyTestedPos { get; set; }

        [Indexed]
        public Guid UserId { get; set; }


        [Ignore]
        public IEnumerable<ClientRelationship> MyRelationships { get; set; }=new List<ClientRelationship>();
        [Ignore]
        public IEnumerable<ClientRelationship> RelatedToMe { get; set; } = new List<ClientRelationship>();
        [Ignore]
        public ICollection<ClientState> ClientStates { get; set; } = new List<ClientState>();

        public Client()
        {
            Id = LiveGuid.NewGuid();
        }

        public Client(Guid id) : base(id)
        {
        }

        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId,Guid userId):this()
        {
            MaritalStatus = maritalStatus;
            KeyPop = keyPop;
            OtherKeyPop = otherKeyPop;
            PracticeId = practiceId;
            UserId = userId;
        }

        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Guid personId,Guid userId)
            :this(maritalStatus, keyPop, otherKeyPop,practiceId, userId)
        {
            PersonId = personId;
        }
        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Person person, Guid userId)
            : this(maritalStatus, keyPop, otherKeyPop, practiceId, userId)
        {
            Person = person;
        }

        public static Client Create(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Person person, Guid userId)
        {
            return new Client(maritalStatus, keyPop, otherKeyPop,practiceId,person,userId);
        }
        public static Client CreateFromPerson(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId,  Guid personId, Guid userId)
        {
            return new Client(maritalStatus, keyPop, otherKeyPop, practiceId, personId,userId);
        }

        public bool IsHtstEnrolled()
        {
            return IsInState(LiveState.HtsEnrolled);
        }
        public bool DisableHts()
        {
            return null != PreventEnroll && PreventEnroll.Value;
        }

        public bool IsInState(params LiveState[] states)
        {
            if (null != ClientStates && ClientStates.Any() && states.Length > 0)
            {
                var found = ClientStates.Where(x => states.Contains(x.Status)).ToList();
                return found.Count == states.Length;
            }
            return false;
        }

        public bool IsInAnyState(params LiveState[] states)
        {
            if (null != ClientStates && ClientStates.Any() && states.Length > 0)
            {
                var found = ClientStates.Where(x => states.Contains(x.Status)).ToList();
                return found.Count > 0;
            }

            return false;
        }

        public bool IsInState(Guid indexId, params LiveState[] states)
        {
            if (null != ClientStates && ClientStates.Any(x => null != x.IndexClientId && x.IndexClientId == indexId) &&
                states.Length > 0)
            {
                var found = ClientStates.Where(x => states.Contains(x.Status) && x.IndexClientId == indexId).ToList();
                return found.Count == states.Length;
            }

            return false;
        }
        public bool IsInAnyState(Guid indexId, params LiveState[] states)
        {
            if (null != ClientStates && ClientStates.Any(x => null != x.IndexClientId && x.IndexClientId == indexId) &&
                states.Length > 0)
            {
                var found = ClientStates.Where(x => states.Contains(x.Status) && x.IndexClientId == indexId).ToList();
                return found.Count > 0;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Person} ,{Person.Gender}";
        }

        public bool IsInFamilyTesting(Guid indexId)
        {
            return IsInState(indexId,LiveState.FamilyListed);
        }

        public bool IsInPns(Guid indexId)
        {
            return IsInState(indexId,LiveState.PartnerListed);
        }
    }
}