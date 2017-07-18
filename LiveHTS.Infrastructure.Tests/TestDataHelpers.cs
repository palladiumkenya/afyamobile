using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using FizzWare.NBuilder;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Tests.Repository;
using NUnit.Framework.Interfaces;
using SQLite;


namespace LiveHTS.Infrastructure.Tests
{
    public class TestDataHelpers
    {
        private readonly string _providerTypeId = "HW"; //HW|Health Worker|0
        private readonly Guid _encounterTypeId = new Guid("7e5164a6-6b99-11e7-907b-a6006ad3dba0"); // |HTS Initial|0
        private readonly string _identifierTypeId = "Serial"; // |Serial|0

        private readonly Guid _practiceId =
            new Guid("7e51629e-6b99-11e7-907b-a6006ad3dba0"); //     |13023|Kenyatta National Hospital|Facility|47|0

        private readonly string _relationshipType = "Partner"; //   |Partner|Partner|0
        private readonly int _countyId = 47;
        private readonly Guid _deviceId = new Guid("7e51658c-6b99-11e7-907b-a6006ad3dba0"); //       |12345|V01|VCT-01|0

        public List<Person> GetTestPeople(int count=5)
        {
            var people = Builder<Person>.CreateListOfSize(count).Build().ToList();
            foreach (var p in people)
            {
                var addresses = Builder<PersonAddress>.CreateListOfSize(2)
                    .All()
                    .With(x => x.PersonId == p.Id)
                    .With(x => x.Preferred ==false)
                    .Build()
                    .ToList();
                addresses.First().Preferred = true;
                
                var contacts = Builder<PersonContact>.CreateListOfSize(2)
                    .All()
                    .With(x => x.PersonId == p.Id)
                    .With(x => x.Preferred == false)
                    .Build()
                    .ToList();
                contacts.First().Preferred = true;

                p.Addresses = addresses;
                p.Contacts = contacts;
            }
            return people;
        }
    }
}