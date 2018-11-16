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
using LiveHTS.Infrastructure.Repository.SmartCard;
using LiveHTS.Infrastructure.Repository.Subject;
using LiveHTS.SharedKernel.Model;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Interview
{
    [TestFixture]
    public class PartnerTracingServiceTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database;

        private IRegistryService _registryService;
        private IPartnerTracingService _partnerTracingService;
        private List<Client> _clients;
        private Client _client;
        private Guid _indexId = new Guid("f2243aff-9f91-4dd0-85fc-a89401258075");

        private IClientRepository _clientRepository;
        private IClientIdentifierRepository _clientIdentifierRepository;
        private IPersonRepository _personRepository;
        private IClientRelationshipRepository _clientRelationshipRepository;
        private IEncounterRepository _encounterRepository;
        private ClientStateRepository _clientStateRepository;

        private IObsPartnerTraceResultRepository _partnerTracingRepository;
        private ICategoryRepository _categoryRepository;

        private Program _partnertracing = new Program(new Guid("b25ec112-852f-11e7-bb31-be2e45b06b35"),
            new Guid("b262fda4-877f-11e7-bb31-be2e44b66b34"));

        private Program _partnerTracing = new Program(new Guid("b25ec112-852f-11e7-bb31-be2e45b06b36"),
            new Guid("b262fda4-877f-11e7-bb31-be2e44b67b34"));

        private PSmartStoreRepository _pSmartStoreRepository;

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
            _partnerTracingRepository = new ObsPartnerTraceResultRepository(_liveSetting);
            _pSmartStoreRepository = new PSmartStoreRepository(_liveSetting);
            _registryService = new RegistryService(_clientRepository, _clientIdentifierRepository, _personRepository,
                _clientRelationshipRepository, _encounterRepository, _clientStateRepository,_pSmartStoreRepository);

            _partnerTracingService = new PartnerTracingService(_encounterRepository, _partnerTracingRepository,
                 _clientStateRepository);
        }

        [Test]
        public void should_Save_Tracing()
        {
            var partner = _clients[0];
            _registryService.SaveOrUpdate(partner, false);
            _registryService.UpdateRelationShips("Partner", _indexId, partner.Id);

            var encounter = _partnerTracingService.StartEncounter(_partnertracing.FormId,
                _partnertracing.EncounterTypeId, partner.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var tracing = Builder<ObsPartnerTraceResult>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Outcome = new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34")) // Contacted
                .With(x => x.IndexClientId = _indexId).Build();

            _partnerTracingService.SaveTest(tracing, partner.Id, _indexId);

            var saved = _partnerTracingService.OpenEncounter(encounter.Id);
            Assert.NotNull(saved);
            Assert.AreEqual(partner.Id, saved.ClientId);
            Assert.AreEqual(_indexId, saved.IndexClientId);
            var partnertracing = saved.ObsPartnerTraceResults.FirstOrDefault();
            Assert.NotNull(partnertracing);
            Assert.AreEqual(encounter.Id, partnertracing.EncounterId);
            Assert.AreEqual(new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34"), partnertracing.Outcome);
            Console.WriteLine(saved);
       
        }
        [Test]
        public void should_Delete_Tracing()
        {
            var partner = _clients[1];
            _registryService.SaveOrUpdate(partner, false);
            _registryService.UpdateRelationShips("Partner", _indexId, partner.Id);
            var encounter = _partnerTracingService.StartEncounter(_partnertracing.FormId,
                _partnertracing.EncounterTypeId, partner.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var tracing = Builder<ObsPartnerTraceResult>.CreateListOfSize(2).All()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Outcome = new Guid("b25f102c-852f-11e7-bb31-be2e44b06b34")) // Not Contacted
                .With(x => x.IndexClientId = _indexId).Build().ToList();
            _partnerTracingService.SaveTest(tracing[0], partner.Id, _indexId);
            _partnerTracingService.SaveTest(tracing[1], partner.Id, _indexId);

            _partnerTracingService.DeleteTest(tracing[0],partner.Id,_indexId);
            var deletedTrace = _partnerTracingRepository.Get(tracing[0].Id);
            Assert.Null(deletedTrace);
        }
        [Test]
        public void should_Delete_Tracing_States()
        {
            var partner = _clients[2];
            _registryService.SaveOrUpdate(partner, false);
            _registryService.UpdateRelationShips("Partner", _indexId, partner.Id);
            var encounter = _partnerTracingService.StartEncounter(_partnertracing.FormId,
                _partnertracing.EncounterTypeId, partner.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var tracing = Builder<ObsPartnerTraceResult>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Outcome = new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34")) // Contacted
                .With(x => x.IndexClientId = _indexId).Build();
            _partnerTracingService.SaveTest(tracing, partner.Id, _indexId);

            _partnerTracingService.DeleteTest(tracing,partner.Id,_indexId);
            var screenedPartner = _registryService.Find(partner.Id);
            Assert.NotNull(screenedPartner);
            var states = screenedPartner.ClientStates.ToList();
            Assert.True(states.Count == 1);
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.PartnerListed));

        }
        [Test]
        public void should_Save_Tracing_Multiple()
        {
            var partner = _clients[3];
            var sister = _clients[4];

            _registryService.SaveOrUpdate(partner, false);
            _registryService.SaveOrUpdate(sister, false);

            _registryService.UpdateRelationShips("Partner", _indexId, partner.Id);
            _registryService.UpdateRelationShips("Sister", _indexId, sister.Id);

            var encounter = _partnerTracingService.StartEncounter(_partnertracing.FormId,
                _partnertracing.EncounterTypeId, partner.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);

            var encounterSister = _partnerTracingService.StartEncounter(_partnertracing.FormId,
                _partnertracing.EncounterTypeId, sister.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);

            var tracing = Builder<ObsPartnerTraceResult>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Outcome = new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34")) // Contacted
                .With(x => x.IndexClientId = _indexId).Build();

            var tracingSister = Builder<ObsPartnerTraceResult>.CreateNew()
                .With(x => x.EncounterId = encounterSister.Id)
                .With(x => x.Outcome = new Guid("b25f102c-852f-11e7-bb31-be2e44b06b34")) // Not Contacted
                .With(x => x.IndexClientId = _indexId).Build();

            _partnerTracingService.SaveTest(tracing, partner.Id, _indexId);
            _partnerTracingService.SaveTest(tracingSister, sister.Id, _indexId);
            
            var saved = _partnerTracingService.OpenEncounter(encounter.Id);
            Assert.NotNull(saved);
            Assert.AreEqual(partner.Id, saved.ClientId);
            Assert.AreEqual(_indexId, saved.IndexClientId);

            var savedSister = _partnerTracingService.OpenEncounter(encounterSister.Id);
            Assert.NotNull(savedSister);
            Assert.AreEqual(sister.Id, savedSister.ClientId);
            Assert.AreEqual(_indexId, savedSister.IndexClientId);
        
            var partnertracing = saved.ObsPartnerTraceResults.FirstOrDefault();
            Assert.NotNull(partnertracing);
            Assert.AreEqual(encounter.Id, partnertracing.EncounterId);
            Assert.AreEqual(new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34"), partnertracing.Outcome);
            Console.WriteLine(saved);

            var partnertracingSister = savedSister.ObsPartnerTraceResults.FirstOrDefault();
            Assert.NotNull(partnertracingSister);
            Assert.AreEqual(encounterSister.Id, partnertracingSister.EncounterId);
            Assert.AreEqual(new Guid("b25f102c-852f-11e7-bb31-be2e44b06b34"), partnertracingSister.Outcome);
            Console.WriteLine(savedSister);
        }


        [Test]
        public void should_Save_Tracing_States()
        {
            var partner = _clients[5];
            _registryService.SaveOrUpdate(partner, false);
            _registryService.UpdateRelationShips("Partner", _indexId, partner.Id);
            var encounter = _partnerTracingService.StartEncounter(_partnertracing.FormId,
                _partnertracing.EncounterTypeId, partner.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var tracing = Builder<ObsPartnerTraceResult>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Outcome = new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34")) // Contacted
                .With(x => x.IndexClientId = _indexId).Build();

            _partnerTracingService.SaveTest(tracing, partner.Id, _indexId);
        
            var screenedPartner = _registryService.Find(partner.Id);
            Assert.NotNull(screenedPartner);
            var states = screenedPartner.ClientStates.ToList();
            Assert.True(states.Count == 2);
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.PartnerListed));
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.PartnerTracedContacted));
            Console.WriteLine(screenedPartner);
            foreach (var state in states)
            {
                Console.WriteLine($" {state}");
            }
        }
    }
}