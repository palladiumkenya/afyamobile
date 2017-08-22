using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.DTO
{
    public class ClientDTO
    {
        public Guid Id { get; set; }
        public  string FirstName { get; set; }
        public  string MiddleName { get; set; }
        public  string LastName { get; set; }
        public  string Gender { get; set; }
        public  DateTime? BirthDate { get; set; }
        public string Age { get; set; }
        public string IdentifierTypeId { get; set; }
        public string Identifier { get; set; }
        public bool HasPartners { get; set; }
        public List<Guid> Partners { get; set; }

        public ClientDTO()
        {
        }

        private ClientDTO(Guid id, string firstName, string middleName, string lastName, string gender, DateTime? birthDate, string age, string identifierTypeId, string identifier)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            Age = age;
            IdentifierTypeId = identifierTypeId;
            Identifier = identifier;            
        }

        public static ClientDTO Create(Client client)
        {
            var ids = client.Identifiers.FirstOrDefault();
            

            var dto= new ClientDTO(client.Id,
                client.Person.FirstName,
                client.Person.MiddleName,
                client.Person.LastName,
                client.Person.Gender,
                client.Person.BirthDate,
                client.Person.AgeInfo,
                ids.IdentifierTypeId,
                ids.Identifier);

            dto.HasPartners = client.Relationships.Any();

            if (dto.HasPartners)
            {
                var partnerIds=new List<Guid>();
                var partners= client.Relationships.Select(x => x.ClientId).ToList();

                if(partners.Count>0)
                    partnerIds.AddRange(partners);

                var otherPartnerss = client.Relationships.Select(x => x.RelatedClientId).ToList();

                if (otherPartnerss.Count > 0)
                    partnerIds.AddRange(otherPartnerss);

                if (partnerIds.Count > 0) ;

                dto.Partners = partnerIds.Where(x =>!x.Equals(dto.Id)).ToList();
            }


            return dto;
        }
    }
}