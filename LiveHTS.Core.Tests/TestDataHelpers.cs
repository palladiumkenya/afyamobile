using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Tests
{
    public class TestDataHelpers
    {
        public static readonly string _providerTypeId = "HW"; //HW|Health Worker|0
        public static readonly Guid _encounterTypeId = new Guid("7e5164a6-6b99-11e7-907b-a6006ad3dba0"); // |HTS Initial|0
        public static readonly string _identifierTypeId = "Serial"; // |Serial|0
        public static readonly Guid _practiceId = new Guid("7e51629e-6b99-11e7-907b-a6006ad3dba0"); //     |13023|Kenyatta National Hospital|Facility|47|0
        public static readonly string _relationshipType = "Partner"; //   |Partner|Partner|0
        public static readonly int _countyId = 47;
        public static readonly Guid _deviceId = new Guid("7e51658c-6b99-11e7-907b-a6006ad3dba0"); //       |12345|V01|VCT-01|0
        public static readonly Guid _formId = new Guid("62040dcc-6260-11e7-907b-a6006ad3dba0"); //       |HTS Lab Form|HTS Lab Form|HTS Lab Form|1|62040ce6-6260-11e7-907b-a6006ad3dba0|0
        public static readonly List<Guid> _questionIds = new List<Guid>()
        {
            new Guid("6206a9a6-6260-11e7-907b-a6006ad3dba0"),
            new Guid("6206aa78-6260-11e7-907b-a6006ad3dba0"),
            new Guid("6206ab4a-6260-11e7-907b-a6006ad3dba0"),
            new Guid("6206ac1c-6260-11e7-907b-a6006ad3dba0")
        };
        public static List<Client> _clients;
        public static List<Provider> _providers;
        public static List<Encounter> Encounters;
        public static List<User> Users;

        public static List<Client> GetTestClients(int count)
        {
            var clients = new List<Client>();
            var people = Builder<Person>.CreateListOfSize(count)
                .All()
                .With(x => x.Voided == false)
                .Build().ToList();

            foreach (var p in people)
            {
                var addresses = Builder<PersonAddress>.CreateListOfSize(2)
                    .All()
                    .With(x => x.PersonId == p.Id)
                    .With(x => x.Preferred == false)
                    .With(x => x.Voided == false)
                    .Build()
                    .ToList();
                addresses.First().Preferred = true;

                var contacts = Builder<PersonContact>.CreateListOfSize(2)
                    .All()
                    .With(x => x.PersonId == p.Id)
                    .With(x => x.Preferred == false)
                    .With(x => x.Voided == false)
                    .Build()
                    .ToList();
                contacts.First().Preferred = true;

                p.Addresses = addresses;
                p.Contacts = contacts;

                var client = Builder<Client>.CreateNew()
                    .With(x => x.PracticeId = _practiceId)
                    .With(x => x.Voided == false)
                    .Build();
                client.PersonId = p.Id;
                client.Person = p;
                clients.Add(client);
            }
            _clients = clients;
            return clients;
        }
        public static List<User> GetTestUsers(int count)
        {
            var users = new List<User>();
            var people = Builder<Person>.CreateListOfSize(count)
                .All()
                .With(x => x.Voided == false)
                .Build().ToList();

            foreach (var p in people)
            {
                var client = Builder<User>.CreateNew()
                    .With(x => x.Voided == false)
                    .With(x => x.PracticeId = _practiceId).Build();
                client.PersonId = p.Id;
                client.Person = p;
                users.Add(client);
            }
            Users = users;
            return users;
        }
        public static List<Provider> GetTestProviders(int count)
        {
            var providers = new List<Provider>();
            var people = Builder<Person>.CreateListOfSize(count)
                .All()
                .With(x => x.Voided == false)
                .Build().ToList();

            foreach (var p in people)
            {
                var client = Builder<Provider>.CreateNew()
                    .With(x => x.Voided == false)
                    .With(x => x.ProviderTypeId = _providerTypeId)
                    .With(x => x.PracticeId = _practiceId).Build();
                client.PersonId = p.Id;
                client.Person = p;
                providers.Add(client);
            }
            _providers = providers;
            return providers;
        }
        public static List<Encounter> GetTestEncounters(int count, List<Client> clients, List<User> users, List<Provider> providers)
        {
            var client = clients.First();
            var user = users.First();
            var provider = providers.First();

            var encounters = Builder<Encounter>.CreateListOfSize(count)
                .All()
                .With(x => x.ClientId = client.Id)
                .With(x => x.FormId = _formId)
                .With(x => x.EncounterTypeId = _encounterTypeId)
                .With(x => x.ProviderId = provider.Id)
                .With(x => x.DeviceId = _deviceId)
                .With(x => x.PracticeId = _practiceId)
                .With(x => x.Voided == false)
                .With(x => x.IsComplete == false)
                .With(x => x.Status == "Opened")
                .Build().ToList();

            foreach (var e in encounters)
            {
                var obs = Builder<Obs>.CreateListOfSize(4)
                    .All()
                    .With(x => x.QuestionId = e.Id)
                    .With(x => x.EncounterId = e.Id)
                    .With(x => x.Voided == false)
                    .Build().ToList();

                obs[0].QuestionId = _questionIds[0];
                obs[1].QuestionId = _questionIds[1];
                obs[2].QuestionId = _questionIds[2];
                obs[3].QuestionId = _questionIds[3];

                e.Obses = obs;
            }
            Encounters = encounters;
            return encounters;
        }
    }

}