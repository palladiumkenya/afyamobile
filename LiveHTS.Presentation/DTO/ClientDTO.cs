using System;
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

            return new ClientDTO(client.Id,
                client.Person.FirstName,
                client.Person.MiddleName,
                client.Person.LastName,
                client.Person.Gender,
                client.Person.BirthDate,
                client.Person.AgeInfo,
                ids.IdentifierTypeId,
                ids.Identifier);
        }
    }
}