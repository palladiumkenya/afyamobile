using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Lookup;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.Service.Clients;
using LiveHTS.Core.Service.Interview;
using LiveHTS.Infrastructure.Repository.Interview;
using LiveHTS.Infrastructure.Repository.Subject;
using LiveHTS.SharedKernel.Model;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Interview
{
    [TestFixture]
    public class MemberTracingServiceTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database;

        private IRegistryService _registryService;
        private IMemberTracingService _memberTracingService;
        private List<Client> _clients;
        private Client _client;
        private Guid _indexId = new Guid("f2243aff-9f91-4dd0-85fc-a89401258075");

        private IClientRepository _clientRepository;
        private IClientIdentifierRepository _clientIdentifierRepository;
        private IPersonRepository _personRepository;
        private IClientRelationshipRepository _clientRelationshipRepository;
        private IEncounterRepository _encounterRepository;
        private ClientStateRepository _clientStateRepository;

        private IObsFamilyTraceResultRepository _memberTracingRepository;
        private ICategoryRepository _categoryRepository;

        private Program _membertracing = new Program(new Guid("b25ec112-852f-11e7-bb31-be2e45b06b35"),
            new Guid("b262fda4-877f-11e7-bb31-be2e44b66b34"));

        private Program _memberTracing = new Program(new Guid("b25ec112-852f-11e7-bb31-be2e45b06b36"),
            new Guid("b262fda4-877f-11e7-bb31-be2e44b67b34"));

        [OneTimeSetUp]
        public void Init()
        {
            _clients = TestDataHelpers.GetTestClients(6);
            _client = _clients.First();
            var dbpath = $@"{TestContext.CurrentContext.TestDirectory}\livehts.db";
            _database = new SQLiteConnection(dbpath);
            _liveSetting = new LiveSetting(_database.DatabasePath);
            Console.WriteLine(dbpath);
        }

        [SetUp]
        public void SetUp()
        {
            _clientRepository = new ClientRepository(_liveSetting);
            _clientIdentifierRepository = new ClientIdentifierRepository(_liveSetting);
            _personRepository = new PersonRepository(_liveSetting);
            _clientRelationshipRepository = new ClientRelationshipRepository(_liveSetting);
            _encounterRepository = new EncounterRepository(_liveSetting);
            _clientStateRepository = new ClientStateRepository(_liveSetting);
            _memberTracingRepository = new ObsFamilyTraceResultRepository(_liveSetting);
            _registryService = new RegistryService(_clientRepository, _clientIdentifierRepository, _personRepository,
                _clientRelationshipRepository, _encounterRepository, _clientStateRepository);

            _memberTracingService = new MemberTracingService(_encounterRepository, _memberTracingRepository,
                 _clientStateRepository);
        }

        [Test]
        public void should_Save_Tracing()
        {
            var son = _clients[0];
            _registryService.SaveOrUpdate(son, false);
            _registryService.UpdateRelationShips("Son", _indexId, son.Id);

            var encounter = _memberTracingService.StartEncounter(_membertracing.FormId,
                _membertracing.EncounterTypeId, son.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var tracing = Builder<ObsFamilyTraceResult>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Outcome = new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34")) // Contacted
                .With(x => x.IndexClientId = _indexId).Build();

            _memberTracingService.SaveTest(tracing, son.Id, _indexId);

            var saved = _memberTracingService.OpenEncounter(encounter.Id);
            Assert.NotNull(saved);
            Assert.AreEqual(son.Id, saved.ClientId);
            Assert.AreEqual(_indexId, saved.IndexClientId);
            var membertracing = saved.ObsFamilyTraceResults.FirstOrDefault();
            Assert.NotNull(membertracing);
            Assert.AreEqual(encounter.Id, membertracing.EncounterId);
            Assert.AreEqual(new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34"), membertracing.Outcome);
            Console.WriteLine(saved);
       
        }
        [Test]
        public void should_Delete_Tracing()
        {
            var son = _clients[1];
            _registryService.SaveOrUpdate(son, false);
            _registryService.UpdateRelationShips("Son", _indexId, son.Id);
            var encounter = _memberTracingService.StartEncounter(_membertracing.FormId,
                _membertracing.EncounterTypeId, son.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var tracing = Builder<ObsFamilyTraceResult>.CreateListOfSize(2).All()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Outcome = new Guid("b25f102c-852f-11e7-bb31-be2e44b06b34")) // Not Contacted
                .With(x => x.IndexClientId = _indexId).Build().ToList();
            _memberTracingService.SaveTest(tracing[0], son.Id, _indexId);
            _memberTracingService.SaveTest(tracing[1], son.Id, _indexId);

            _memberTracingService.DeleteTest(tracing[0],son.Id,_indexId);
            var deletedTrace = _memberTracingRepository.Get(tracing[0].Id);
            Assert.Null(deletedTrace);
        }
        [Test]
        public void should_Delete_Tracing_States()
        {
            var son = _clients[2];
            _registryService.SaveOrUpdate(son, false);
            _registryService.UpdateRelationShips("Son", _indexId, son.Id);
            var encounter = _memberTracingService.StartEncounter(_membertracing.FormId,
                _membertracing.EncounterTypeId, son.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var tracing = Builder<ObsFamilyTraceResult>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Outcome = new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34")) // Contacted
                .With(x => x.IndexClientId = _indexId).Build();
            _memberTracingService.SaveTest(tracing, son.Id, _indexId);

            _memberTracingService.DeleteTest(tracing,son.Id,_indexId);
            var screenedSon = _registryService.Find(son.Id);
            Assert.NotNull(screenedSon);
            var states = screenedSon.ClientStates.ToList();
            Assert.True(states.Count == 1);
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.FamilyListed));

        }
        [Test]
        public void should_Save_Tracing_Multiple()
        {
            var son = _clients[3];
            var sister = _clients[4];

            _registryService.SaveOrUpdate(son, false);
            _registryService.SaveOrUpdate(sister, false);

            _registryService.UpdateRelationShips("Son", _indexId, son.Id);
            _registryService.UpdateRelationShips("Sister", _indexId, sister.Id);

            var encounter = _memberTracingService.StartEncounter(_membertracing.FormId,
                _membertracing.EncounterTypeId, son.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);

            var encounterSister = _memberTracingService.StartEncounter(_membertracing.FormId,
                _membertracing.EncounterTypeId, sister.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);

            var tracing = Builder<ObsFamilyTraceResult>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Outcome = new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34")) // Contacted
                .With(x => x.IndexClientId = _indexId).Build();

            var tracingSister = Builder<ObsFamilyTraceResult>.CreateNew()
                .With(x => x.EncounterId = encounterSister.Id)
                .With(x => x.Outcome = new Guid("b25f102c-852f-11e7-bb31-be2e44b06b34")) // Not Contacted
                .With(x => x.IndexClientId = _indexId).Build();

            _memberTracingService.SaveTest(tracing, son.Id, _indexId);
            _memberTracingService.SaveTest(tracingSister, sister.Id, _indexId);
            
            var saved = _memberTracingService.OpenEncounter(encounter.Id);
            Assert.NotNull(saved);
            Assert.AreEqual(son.Id, saved.ClientId);
            Assert.AreEqual(_indexId, saved.IndexClientId);

            var savedSister = _memberTracingService.OpenEncounter(encounterSister.Id);
            Assert.NotNull(savedSister);
            Assert.AreEqual(sister.Id, savedSister.ClientId);
            Assert.AreEqual(_indexId, savedSister.IndexClientId);
        
            var membertracing = saved.ObsFamilyTraceResults.FirstOrDefault();
            Assert.NotNull(membertracing);
            Assert.AreEqual(encounter.Id, membertracing.EncounterId);
            Assert.AreEqual(new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34"), membertracing.Outcome);
            Console.WriteLine(saved);

            var membertracingSister = savedSister.ObsFamilyTraceResults.FirstOrDefault();
            Assert.NotNull(membertracingSister);
            Assert.AreEqual(encounterSister.Id, membertracingSister.EncounterId);
            Assert.AreEqual(new Guid("b25f102c-852f-11e7-bb31-be2e44b06b34"), membertracingSister.Outcome);
            Console.WriteLine(savedSister);
        }


        [Test]
        public void should_Save_Tracing_States()
        {
            var son = _clients[5];
            _registryService.SaveOrUpdate(son, false);
            _registryService.UpdateRelationShips("Son", _indexId, son.Id);
            var encounter = _memberTracingService.StartEncounter(_membertracing.FormId,
                _membertracing.EncounterTypeId, son.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var tracing = Builder<ObsFamilyTraceResult>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Outcome = new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34")) // Contacted
                .With(x => x.IndexClientId = _indexId).Build();

            _memberTracingService.SaveTest(tracing, son.Id, _indexId);
        
            var screenedSon = _registryService.Find(son.Id);
            Assert.NotNull(screenedSon);
            var states = screenedSon.ClientStates.ToList();
            Assert.True(states.Count == 2);
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.FamilyListed));
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.FamilyTracedContacted));
            Console.WriteLine(screenedSon);
            foreach (var state in states)
            {
                Console.WriteLine($" {state}");
            }
        }
    }
}