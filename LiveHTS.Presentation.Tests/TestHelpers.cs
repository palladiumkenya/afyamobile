using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Presentation.Tests
{
    public class TestHelpers
    {
       
        public static List<Client> CreateTestClients(int count=1, int addressCount = 1, int personCount = 1, int relationsCount=1)
        {
            var clients = new List<Client>();

            var people = Builder<Person>.CreateListOfSize(count)
                .All()
                .With(x => x.Voided = false)
                .Build().ToList();

            people[0].FirstName = "John";
            people[0].LastName = "Doe";

            foreach (var p in people)
            {
                var addresses = Builder<PersonAddress>.CreateListOfSize(addressCount)
                    .All()
                    .With(x => x.PersonId = p.Id)
                    .With(x => x.Preferred = false)
                    .With(x => x.Voided = false)
                    .Build()
                    .ToList();
                addresses.First().Preferred = true;

                var contacts = Builder<PersonContact>.CreateListOfSize(personCount)
                    .All()
                    .With(x => x.PersonId = p.Id)
                    .With(x => x.Preferred = false)
                    .With(x => x.Voided = false)
                    .Build()
                    .ToList();
                contacts.First().Preferred = true;

                p.Addresses = addresses;
                p.Contacts = contacts;

                var client = Builder<Client>.CreateNew()
                    .With(x => x.Voided = false)
                    .Build();
                client.PersonId = p.Id;
                client.Person = p;

                var identifiers = Builder<ClientIdentifier>.CreateListOfSize(1).All()
                    .With(x => x.Identifier = $"IDS-{DateTime.Now.Ticks}")
                    .With(x => x.ClientId = client.Id)
                    .Build();
                var relationships = Builder<ClientRelationship>.CreateListOfSize(relationsCount).All()
                    .With(x => x.ClientId = client.Id)
                    .Build();
                client.Identifiers = identifiers;
                client.Relationships = relationships;
                clients.Add(client);
            }

            return clients;
        }
    }
}