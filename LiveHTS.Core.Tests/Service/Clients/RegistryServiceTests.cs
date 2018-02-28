using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Service.Clients;
using LiveHTS.Infrastructure.Repository.Interview;
using LiveHTS.Infrastructure.Repository.Subject;
using LiveHTS.Infrastructure.Repository.Survey;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Clients
{
    [TestFixture]
    public class RegistryServiceTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database;

        private IRegistryService _registryService;
        private List<Client> _clients;
        private Client _client;
        private Guid _indexId=new Guid("f2243aff-9f91-4dd0-85fc-a89401258075");

        private IClientRepository _clientRepository;
        private IClientIdentifierRepository _clientIdentifierRepository;
        private IPersonRepository _personRepository;
        private IClientRelationshipRepository _clientRelationshipRepository;
        private IEncounterRepository _encounterRepository;
        private ClientStateRepository _clientStateRepository;

        [OneTimeSetUp]
        public void Init()
        {
            _clients = TestDataHelpers.GetTestClients(3);
            _client = _clients.First();
            var dbpath = $@"{TestContext.CurrentContext.TestDirectory}\livehts.db";
            _database =new SQLiteConnection(dbpath);
            _liveSetting = new LiveSetting(_database.DatabasePath);
            Console.WriteLine(dbpath);
        }

        [SetUp]
        public void SetUp()
        {
            _clientRepository =new ClientRepository(_liveSetting);
            _clientIdentifierRepository=new ClientIdentifierRepository(_liveSetting);
            _personRepository=new PersonRepository(_liveSetting);
            _clientRelationshipRepository=new ClientRelationshipRepository(_liveSetting);
            _encounterRepository=new EncounterRepository(_liveSetting);
            _clientStateRepository=new ClientStateRepository(_liveSetting);
            _registryService=new RegistryService(_clientRepository,_clientIdentifierRepository,_personRepository, _clientRelationshipRepository, _encounterRepository,_clientStateRepository);
        }
        [Test]
        public void should_Find_Client()
        {
            var cient = _registryService.Find(_indexId);
            Assert.IsNotNull(cient);
            Assert.IsNotNull(cient.Person);
            Assert.IsTrue(cient.Identifiers.Any());
            Assert.IsTrue(cient.ClientStates.Any());
            Console.WriteLine(cient);
            foreach (var identifier in cient.Identifiers)
            {
                Console.WriteLine($"  > {identifier}");
            }
        }


        [Test]
        public void should_GetAllClients()
        {
            var clients = _registryService.GetAllClients().ToList();
            Assert.IsTrue(clients.Count > 0);
            var cient = clients.First();
            Assert.IsNotNull(cient);
            Assert.IsNotNull(cient.Person);
            Assert.IsTrue(cient.Identifiers.Any());
            Assert.IsTrue(cient.ClientStates.Any());
            Console.WriteLine(cient);

        }

        [Test]
        public void should_GetAllClients_Search()
        {
            var clients = _registryService.GetAllClients("Papa").ToList();
            Assert.IsTrue(clients.Count > 0);
            var cient = clients.First();
            Assert.IsNotNull(cient);
            Assert.IsNotNull(cient.Person);
            Assert.IsTrue(cient.Identifiers.Any());
            Assert.IsTrue(cient.ClientStates.Any());
            Console.WriteLine(cient);
        }

        [Test]
        public void should_SaveOrUpdate_New()
        {
            _registryService.SaveOrUpdate(_client);

            var cientNew = _registryService.Find(_client.Id);
            Assert.IsNotNull(cientNew);
            var states = cientNew.ClientStates.ToList();
            Assert.True(states.Count==2);

            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.HtsEnrolled));
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.HtsFamAcceptedYes));
            Console.WriteLine(cientNew);
            foreach (var clientState in states)
            {
                Console.WriteLine($"  {clientState}");
            }
        }

        [Test]
        public void should_SaveOrUpdate_Update()
        {
            var guid = LiveGuid.NewGuid();

            var client = _registryService.Find(_indexId);
            client.Person.FirstName = "Joni";
            client.Identifiers.First().Identifier = "999999";
            _registryService.SaveOrUpdate(client);

            var cientNew = _registryService.Find(_indexId);
            Assert.IsNotNull(cientNew);
            Assert.AreEqual("Joni", cientNew.Person.FirstName);
            Assert.AreEqual("999999", cientNew.Identifiers.First().Identifier);
            var states = cientNew.ClientStates.ToList();
            Assert.True(states.Count==2);

            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.HtsEnrolled));
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.HtsFamAcceptedYes));
            Console.WriteLine(cientNew);
        }

        [Test]
        public void should_SaveOrUpdate_New_Relation()
        {
            var son = _clients.Last();
            _registryService.SaveOrUpdate(son,false);
            var relation = _registryService.Find(son.Id);
            Assert.IsNotNull(relation);
            var states = relation.ClientStates.ToList();
            Assert.True(states.Count == 0);

            _registryService.UpdateRelationShips("Son",_indexId,son.Id);
            var indexSon = _registryService.Find(son.Id);
            Assert.IsNotNull(indexSon);
            var statesNew = indexSon.ClientStates.ToList();
            Assert.True(statesNew.Count > 0);
            Console.WriteLine(relation);
        }

        [Test]
        public void should_SaveOrUpdate_Update_Relation()
        {
            var son = _clients.Last();
            _registryService.SaveOrUpdate(son, false);

            var indexSon = _registryService.Find(son.Id);
            indexSon.Person.FirstName = "Kijana";
            _registryService.SaveOrUpdate(indexSon);

            var updatedIndexSon = _registryService.Find(indexSon.Id);
            Assert.IsNotNull(updatedIndexSon);
            Assert.AreEqual("Kijana", updatedIndexSon.Person.FirstName);
            Console.WriteLine(updatedIndexSon);
        }


        [Test]
        public void should_SaveOrUpdate_New_Relation_With_States()
        {
            var son = _clients.Last();
            _registryService.SaveOrUpdate(son, false);
            _registryService.UpdateRelationShips("Son", _indexId, son.Id);

            var index = _registryService.Find(_indexId);
            var indexStates = index.ClientStates.ToList();
            Assert.True(indexStates.Count == 3);
            Assert.NotNull(indexStates.FirstOrDefault(x => x.Status == LiveState.HtsEnrolled));
            Assert.NotNull(indexStates.FirstOrDefault(x => x.Status == LiveState.HtsFamAcceptedYes));
            Assert.NotNull(indexStates.FirstOrDefault(x => x.Status == LiveState.HtsFamlisted));

            var indexSon = _registryService.Find(son.Id);
            var indexSonStates = indexSon.ClientStates.ToList();
            Assert.True(indexSonStates.Count ==1);
            Assert.NotNull(indexSonStates.FirstOrDefault(x => x.Status == LiveState.FamilyListed));
            Assert.AreEqual(index.Id,indexSonStates.First().IndexClientId);

            Console.WriteLine(index);
            foreach (var indexState in indexStates)
            {
                Console.WriteLine($" {indexState}");
            }
            Console.WriteLine(new string('-',30));
            Console.WriteLine(indexSon);
            foreach (var indexSonState in indexSonStates)
            {
                Console.WriteLine($" {indexSonState}");
            }
        }
        [Test]
        public void should_SaveOrUpdate_New_Partner_Relation_With_States()
        {
            var son = _clients.Last();
            _registryService.SaveOrUpdate(son, false);
            _registryService.UpdateRelationShips("Partner", _indexId, son.Id);

            var index = _registryService.Find(_indexId);
            var indexStates = index.ClientStates.ToList();
            Assert.True(indexStates.Count >= 3);
            Assert.NotNull(indexStates.FirstOrDefault(x => x.Status == LiveState.HtsEnrolled));
            Assert.NotNull(indexStates.FirstOrDefault(x => x.Status == LiveState.HtsFamAcceptedYes));
            Assert.NotNull(indexStates.FirstOrDefault(x => x.Status == LiveState.HtsPatlisted));

            var indexSon = _registryService.Find(son.Id);
            var indexSonStates = indexSon.ClientStates.ToList();
            Assert.True(indexSonStates.Count == 1);
            Assert.NotNull(indexSonStates.FirstOrDefault(x => x.Status == LiveState.PartnerListed));
            Assert.AreEqual(index.Id, indexSonStates.First().IndexClientId);

            Console.WriteLine(index);
            foreach (var indexState in indexStates)
            {
                Console.WriteLine($" {indexState}");
            }
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(indexSon);
            foreach (var indexSonState in indexSonStates)
            {
                Console.WriteLine($" {indexSonState}");
            }
        }

        [Test]
        public void should_Delete_Client()
        {
            var clientToDelete = _clients[1];
            _registryService.SaveOrUpdate(clientToDelete);
        
            _registryService.Delete(clientToDelete.Id);

            var deleted = _registryService.Find(clientToDelete.Id);
            Assert.IsNull(deleted);
        }
        /*
      [Test]
      public void should_Add_New_Relation_With_Index()
      {
          var client = _registryService.Find(_indexId);
          var son = _clients.Last();


          _registryService.UpdateRelationShips("Partner", clients[0].Id, clients[1].Id);
          _registryService.UpdateRelationShips("Partner", clients[0].Id, clients[2].Id);


          var indexClient = _registryService.Find(clients[0].Id);
          Assert.IsNotNull(indexClient);
          Assert.IsNotNull(indexClient.MyRelationships.ToList().Count == 2);
          Assert.IsNotNull(indexClient.RelatedToMe.ToList().Count == 2);
          Console.WriteLine(indexClient);
          foreach (var client in indexClient.MyRelationships)
          {
              Assert.False(client.IsIndex);
              Console.WriteLine($" >> {client.RelationshipTypeId}|{client.RelatedClientId}|{client.IsIndex}");
          }
          Console.WriteLine("Secondary Clients");
          foreach (var client in indexClient.RelatedToMe)
          {
              Assert.True(client.IsIndex);
              Console.WriteLine($" >> {client.RelationshipTypeId}|{client.ClientId}|{client.IsIndex}");
          }
      }






      [Test]
      public void should_Save_New()
      {

          var client = TestDataHelpers.GetTestClients(1).First();

          _registryService.Save(client);

          var cientNew = _registryService.Find(client.Id);
          Assert.IsNotNull(cientNew);
          Assert.IsNotNull(cientNew.Person);
          Assert.IsNotNull(cientNew.Person.Addresses.FirstOrDefault());
          Assert.IsNotNull(cientNew.Person.Contacts.FirstOrDefault());
          Assert.IsNotNull(cientNew.Identifiers.FirstOrDefault());
          Assert.IsNotNull(cientNew.Relationships.FirstOrDefault());
          Console.WriteLine(cientNew);

      }

      [Test]
      public void should_Save_New_With_State()
      {

          var client = TestDataHelpers.GetTestClients(1).First();

          _registryService.Save(client);

          var cientNew = _registryService.Find(client.Id);
          Assert.IsNotNull(cientNew);



      }

      [Test]
      public void should_Save_Exsistng()
      {
          var client = TestDataHelpers.GetTestClients(1).First();
          _registryService.SaveOrUpdate(client);
          var cientExisting = _registryService.Find(client.Id);
          Assert.IsNotNull(cientExisting);

          var person = cientExisting.Person;
          person.FirstName = "Uhuru";
          cientExisting.Person = person;

          _registryService.SaveOrUpdate(cientExisting);
          var updatedClient = _registryService.Find(client.Id);

          Assert.AreEqual("Uhuru", updatedClient.Person.FirstName);
          Assert.AreEqual(client.Person.Addresses.ToList().Count, updatedClient.Person.Addresses.ToList().Count());
          Assert.AreEqual(client.Person.Contacts.ToList().Count, updatedClient.Person.Contacts.ToList().Count());
          Assert.AreEqual(client.Identifiers.ToList().Count, updatedClient.Identifiers.ToList().Count());

          Console.WriteLine(updatedClient);
      }





     
      */
    }
}