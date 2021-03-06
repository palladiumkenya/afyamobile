﻿using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Tests
{
    public class TestDataHelpers
    {
        public static readonly Guid _userId=new Guid("61A9E04C-2ED0-414A-9387-A7B7016DF233");
        public static readonly string _providerTypeId = "HW"; //HW|Health Worker|0
        public static readonly Guid _encounterTypeId = new Guid("7e5164a6-6b99-11e7-907b-a6006ad3dba0"); // |HTS Initial|0
        public static readonly string _identifierTypeId = "Serial"; // |Serial|0
        public static readonly Guid _practiceId = new Guid("AB054358-98B9-11E7-ABC4-CEC278B6B50A"); //     |13023|Kenyatta National Hospital|Facility|47|0
        public static readonly string _relationshipType = "Partner"; //   |Partner|Partner|0
        public static readonly int _countyId = 47;
        public static readonly Guid _deviceId = new Guid("7e51658c-6b99-11e7-907b-a6006ad3dba0"); //       |12345|V01|VCT-01|0
        public static readonly Guid _moduleId = new Guid(" 62040ce6-6260-11e7-907b-a6006ad3dba0"); //      |HTS Module|Hiv Testing Services Module|Hiv Testing Services Module|1|0
        public static readonly Guid _formId = new Guid("62040dcc-6260-11e7-907b-a6006ad3dba0"); //       |HTS Lab Form|HTS Lab Form|HTS Lab Form|1|62040ce6-6260-11e7-907b-a6006ad3dba0|0
        public static readonly List<Guid> _questionIds = new List<Guid>()
        {
            new Guid("b2603dc6-852f-11e7-bb31-be2e44b06b34"), //Consent
            new Guid("6206aa78-6260-11e7-907b-a6006ad3dba0"),
            new Guid("6206ab4a-6260-11e7-907b-a6006ad3dba0"),
            new Guid("6206ac1c-6260-11e7-907b-a6006ad3dba0")
        };
        
        public static readonly Guid _consentYes= new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"); //   Yes,00c2a902-6246-11e7-907b-a6006ad3dba0
        public static readonly Guid _consentNo= new Guid("00c2aae2-6246-11e7-907b-a6006ad3dba0"); //     No,00c2aae2-6246-11e7-907b-a6006ad3dba0

        public static List<Client> _clients;
        public static List<Provider> _providers;
        public static List<Encounter> Encounters;
        public static List<User> Users;

        public static List<Client> GetTestClients(int count)
        {
            var clients = new List<Client>();
            var people = Builder<Person>.CreateListOfSize(count)
                .All()
                .With(x => x.Voided = false)
                .Build().ToList();

            people[0].FirstName = "John";
            people[0].LastName = "Doe";

            int n = 0;
            foreach (var p in people)
            {
                n++;
                var addresses = Builder<PersonAddress>.CreateListOfSize(2)
                    .All()
                    .With(x => x.PersonId = p.Id)
                    .With(x => x.Preferred = false)
                    .With(x => x.Voided =false)
                    .Build()
                    .ToList();
                addresses.First().Preferred = true;

                var contacts = Builder<PersonContact>.CreateListOfSize(2)
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
                    .With(x => x.PracticeId = _practiceId)
                    .With(x => x.UserId = _userId)
                    .With(x => x.Voided = false)
                    .Build();
                client.PersonId = p.Id;
                client.Person = p;

                var identifiers = Builder<ClientIdentifier>.CreateListOfSize(1).All()
                    .With(x => x.IdentifierTypeId = _identifierTypeId)
                    .With(x=>x.Identifier=$"IDS-{DateTime.Now.Ticks}-{n}")
                    .With(x=>x.ClientId=client.Id)
                    .Build();
                var relationships = Builder<ClientRelationship>.CreateListOfSize(2).All()
                    .With(x=>x.RelationshipTypeId= _relationshipType)
                    .With(x => x.ClientId = client.Id)
                    .Build();
                client.Identifiers = identifiers;
                //client.Relationships = relationships;
                clients.Add(client);
            }
            
            _clients = clients;
            return clients;
        }
        public static List<Person> GetTestPersons(int count)
        {
            var people = Builder<Person>.CreateListOfSize(count)
                .All()
                .With(x => x.Voided == false)
                .Build().ToList();

            foreach (var p in people)
            {
                var addresses = Builder<PersonAddress>.CreateListOfSize(2)
                    .All()
                    .With(x => x.PersonId = p.Id)
                    .With(x => x.Preferred = false)
                    .With(x => x.Voided = false)
                    .Build()
                    .ToList();
                addresses.First().Preferred = true;

                var contacts = Builder<PersonContact>.CreateListOfSize(2)
                    .All()
                    .With(x => x.PersonId = p.Id)
                    .With(x => x.Preferred = false)
                    .With(x => x.Voided = false)
                    .Build()
                    .ToList();
                contacts.First().Preferred = true;

                p.Addresses = addresses;
                p.Contacts = contacts;
            }
            return people;
        }
        public static List<User> GetTestUsers(int count)
        {
            var users = new List<User>();
            var people = Builder<Person>.CreateListOfSize(count)
                .All()
                .With(x => x.Voided = false)
                .Build().ToList();

            foreach (var p in people)
            {
                var client = Builder<User>.CreateNew()
                    .With(x => x.Voided = false)
                    .With(x => x.PracticeId = _practiceId).Build();
                client.PersonId = p.Id;
                client.Person = p;
                users.Add(client);
            }
            users[0].UserName = "admin";
            users[0].Password = "maun2806";
            Users = users;
            return users;
        }
        public static List<Provider> GetTestProviders(int count)
        {
            var providers = new List<Provider>();
            var people = Builder<Person>.CreateListOfSize(count)
                .All()
                .With(x => x.Voided =false)
                .Build().ToList();

            foreach (var p in people)
            {
                var client = Builder<Provider>.CreateNew()
                    .With(x => x.Voided = false)
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
                .With(x => x.Voided = false)
                .With(x => x.IsComplete = false)
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
                obs[0].ValueCoded = _consentYes;
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