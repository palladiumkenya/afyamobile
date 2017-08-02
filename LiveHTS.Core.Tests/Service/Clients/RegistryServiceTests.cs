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
        

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine(TestContext.CurrentContext.WorkDirectory);

            _liveSetting = new LiveSetting(_database.DatabasePath);
            

            _clientRepository =new ClientRepository(_liveSetting);
            _clientIdentifierRepository=new ClientIdentifierRepository(_liveSetting);
            _personRepository=new PersonRepository(_liveSetting);

            _registryService=new RegistryService(_clientRepository,_clientIdentifierRepository,_personRepository);
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
        public void should_Delete_Client()
        {
            _registryService.Delete(_client.Id);

            var deleted= _registryService.Find(_client.Id);
            Assert.IsNull(deleted);
        }
    }
}