using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Model
{
    public class SyncClientPriorityDTO
    {
        public Guid IndexId { get; set; }
        public List<Guid> SecondaryIds { get; set; }=new List<Guid>();

        public SyncClientPriorityDTO(Guid indexId)
        {
            IndexId = indexId;
        }

        public void AddSecondaryId(List<Guid> ids)
        {
            SecondaryIds.AddRange(ids);
        }

        public static List<SyncClientPriorityDTO> Create(List<ClientRelationship> clientRelationships)
        {
            var list=new List<SyncClientPriorityDTO>();

            var indexList = clientRelationships.Select(x => x.ClientId).Distinct().ToList();

            foreach (var index in indexList)
            {
                var secondaryList = clientRelationships.Where(x => x.ClientId == index).Select(x => x.RelatedClientId)
                    .ToList();
                var dto = new SyncClientPriorityDTO(index);
                dto.AddSecondaryId(secondaryList);
                list.Add(dto);
            }

            return list;
        }
    }


    public class SyncClientDTO
    {
        public Guid Id { get; set; }
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string OtherKeyPop { get; set; }
        public Guid? PracticeId { get; set; }
        public string PracticeCode { get; set; }
        public Person Person { get; set; }
        public bool IsFamilyMember { get; set; }
        public bool IsPartner { get; set; }
        public bool? PreventEnroll { get; set; }
        public bool? AlreadyTestedPos { get; set; }
        public Guid? Education { get; set; }
        public Guid? Completion { get; set; }
        public Guid? Occupation { get; set; }
        public Guid UserId { get; set; }
        public List<ClientIdentifier> Identifiers { get; set; } = new List<ClientIdentifier>();
        public List<ClientRelationship> Relationships { get; set; } = new List<ClientRelationship>();
        public List<ClientState> ClientStates { get; set; }=new List<ClientState>();

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
            ClientStates = client.ClientStates.ToList();
            IsFamilyMember = client.IsFamilyMember;
            IsPartner = client.IsPartner;
            PreventEnroll = client.PreventEnroll;
            AlreadyTestedPos = client.AlreadyTestedPos;
            Education = client.Education;
            Completion = client.Completion;
            Occupation = client.Occupation;
            UserId = client.UserId;
        }
    }
}
