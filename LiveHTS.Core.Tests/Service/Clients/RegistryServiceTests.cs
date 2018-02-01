using System;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Service.Clients;
using LiveHTS.Infrastructure.Repository.Subject;
using LiveHTS.Infrastructure.Repository.Survey;
using LiveHTS.SharedKernel.Custom;

using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Clients
{
    [TestFixture]
    public class RegistryServiceTests
    {
        private bool setNunit = TestHelpers.UseNunit = true;
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();

        private IRegistryService _registryService;
        private Client _client = TestDataHelpers._clients.First();

        private IClientRepository _clientRepository;
        private IClientIdentifierRepository _clientIdentifierRepository;
        private IPersonRepository _personRepository;
        private IClientRelationshipRepository _clientRelationshipRepository;
        

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine(TestContext.CurrentContext.WorkDirectory);

            _liveSetting = new LiveSetting(_database.DatabasePath);
            

            _clientRepository =new ClientRepository(_liveSetting);
            _clientIdentifierRepository=new ClientIdentifierRepository(_liveSetting);
            _personRepository=new PersonRepository(_liveSetting);
            _clientRelationshipRepository=new ClientRelationshipRepository(_liveSetting);

            _registryService=new RegistryService(_clientRepository,_clientIdentifierRepository,_personRepository, _clientRelationshipRepository);
        }
        
        [Test]
        public void should_Find_Client()
        {
            var cient = _registryService.Find(_client.Id);
            Assert.IsNotNull(cient);
            Assert.IsNotNull(cient.Person);
            Assert.IsTrue(cient.Identifiers.Any());
            Assert.IsTrue(cient.Relationships.Any());
            Console.WriteLine(cient);
            foreach (var identifier in cient.Identifiers)
            {
                Console.WriteLine($"  > {identifier}");
            }
            foreach (var relationship in cient.Relationships)
            {
                Console.WriteLine($"  > {relationship}");
            }            
        }


        [Test]
        public void should_GetAllClients()
        {
            var clients = _registryService.GetAllClients().ToList();
            Assert.IsTrue(clients.Count>0);
            var cient = clients.First();
            Assert.IsNotNull(cient);
            Assert.IsNotNull(cient.Person);
            Assert.IsTrue(cient.Identifiers.Any());
            Assert.IsTrue(cient.Relationships.Any());
            Console.WriteLine(cient);

        }

        [Test]
        public void should_GetAllClients_Search()
        {
            var clients = _registryService.GetAllClients("Doe").ToList();
            Assert.IsTrue(clients.Count > 0);
            var cient = clients.First();
            Assert.IsNotNull(cient);
            Assert.IsNotNull(cient.Person);
            
            Console.WriteLine(cient);
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

        [Test]
        public void should_SaveOrUpdate_New()
        {
            var client = Builder<Client>.CreateNew().Build();
            _registryService.SaveOrUpdate(client);

            var cientNew = _registryService.Find(_client.Id);
            Assert.IsNotNull(cientNew);
            Console.WriteLine(cientNew);
        }

        [Test]
        public void should_SaveOrUpdate_Update()
        {
            var guid = LiveGuid.NewGuid();
            
            var client = _registryService.Find(_client.Id);
            client.PracticeId = guid;

            _registryService.SaveOrUpdate(client);

            var cientNew = _registryService.Find(_client.Id);
            Assert.IsNotNull(cientNew);
            Assert.AreEqual(guid, cientNew.PracticeId);
            Console.WriteLine(cientNew);
        }


        [Test]
        public void should_Create_Relations_With_Index()
        {
            var clients = TestDataHelpers.GetTestClients(3);
            foreach (var client in clients)
            {
                _registryService.Save(client);
            }

            _registryService.UpdateRelationShips("Partner", clients[0].Id, clients[1].Id);
            _registryService.UpdateRelationShips("Partner", clients[0].Id, clients[2].Id);


            var indexClient = _registryService.Find(clients[0].Id);
            Assert.IsNotNull(indexClient);
            Assert.IsNotNull(indexClient.Relationships.ToList().Count>2);
            Console.WriteLine(indexClient);
            foreach (var client in indexClient.Relationships)
            {
                Console.WriteLine($" >> {client.RelationshipTypeId}|{client.RelatedClientId}");
            }
            


        }

        [Test]
        public void should_Delete_Client()
        {
            _registryService.Delete(_client.Id);

            var deleted= _registryService.Find(_client.Id);
            Assert.IsNull(deleted);
        }
    }
}