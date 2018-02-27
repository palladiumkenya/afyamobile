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
    public class MemberScreeningServiceTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database;

        private IRegistryService _registryService;
        private IMemberScreeningService _memberScreeningService;
        private List<Client> _clients;
        private Client _client;
        private Guid _indexId = new Guid("f2243aff-9f91-4dd0-85fc-a89401258075");

        private IClientRepository _clientRepository;
        private IClientIdentifierRepository _clientIdentifierRepository;
        private IPersonRepository _personRepository;
        private IClientRelationshipRepository _clientRelationshipRepository;
        private IEncounterRepository _encounterRepository;
        private ClientStateRepository _clientStateRepository;

        private IObsMemberScreeningRepository _memberScreeningRepository;
        private ICategoryRepository _categoryRepository;

        private Program _memberscreening = new Program(new Guid("b25ec112-852f-11e7-bb31-be2e45b06b35"),
            new Guid("b262fda4-877f-11e7-bb31-be2e44b66b34"));

        private Program _memberTracing = new Program(new Guid("b25ec112-852f-11e7-bb31-be2e45b06b36"),
            new Guid("b262fda4-877f-11e7-bb31-be2e44b67b34"));

        [OneTimeSetUp]
        public void Init()
        {
            _clients = TestDataHelpers.GetTestClients(4);
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
            _memberScreeningRepository = new ObsMemberScreeningRepository(_liveSetting);
            _registryService = new RegistryService(_clientRepository, _clientIdentifierRepository, _personRepository,
                _clientRelationshipRepository, _encounterRepository, _clientStateRepository);

            _memberScreeningService = new MemberScreeningService(_encounterRepository, _memberScreeningRepository,
                _categoryRepository, _clientStateRepository);
        }

        [Test]
        public void should_Save_Screening()
        {
            var son = _clients[0];
            _registryService.SaveOrUpdate(son, false);
            _registryService.UpdateRelationShips("Son", _indexId, son.Id);

            var encounter = _memberScreeningService.StartEncounter(_memberscreening.FormId,
                _memberscreening.EncounterTypeId, son.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var screening = Builder<ObsMemberScreening>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Eligibility = new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"))
                .With(x => x.IndexClientId = _indexId).Build();

            _memberScreeningService.SaveMemberScreening(screening, son.Id, _indexId);

            var saved = _memberScreeningService.OpenEncounter(encounter.Id);
            Assert.NotNull(saved);
            Assert.AreEqual(son.Id, saved.ClientId);
            Assert.AreEqual(_indexId, saved.IndexClientId);
            var memberscreening = saved.ObsMemberScreenings.FirstOrDefault();
            Assert.NotNull(memberscreening);
            Assert.AreEqual(encounter.Id, memberscreening.EncounterId);
            Assert.AreEqual(new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"), memberscreening.Eligibility);
            Console.WriteLine(saved);
       
        }
        [Test]
        public void should_Save_Screening_Multiple()
        {
            var son = _clients[1];
            var sister = _clients[2];

            _registryService.SaveOrUpdate(son, false);
            _registryService.SaveOrUpdate(sister, false);

            _registryService.UpdateRelationShips("Son", _indexId, son.Id);
            _registryService.UpdateRelationShips("Sister", _indexId, sister.Id);

            var encounter = _memberScreeningService.StartEncounter(_memberscreening.FormId,
                _memberscreening.EncounterTypeId, son.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);

            var encounterSister = _memberScreeningService.StartEncounter(_memberscreening.FormId,
                _memberscreening.EncounterTypeId, sister.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);

            var screening = Builder<ObsMemberScreening>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Eligibility = new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"))
                .With(x => x.IndexClientId = _indexId).Build();

            var screeningSister = Builder<ObsMemberScreening>.CreateNew()
                .With(x => x.EncounterId = encounterSister.Id)
                .With(x => x.Eligibility = new Guid("b25ed04e-852f-11e7-bb31-be2e44b06b34"))
                .With(x => x.IndexClientId = _indexId).Build();

            _memberScreeningService.SaveMemberScreening(screening, son.Id, _indexId);
            _memberScreeningService.SaveMemberScreening(screeningSister, sister.Id, _indexId);
            
            var saved = _memberScreeningService.OpenEncounter(encounter.Id);
            Assert.NotNull(saved);
            Assert.AreEqual(son.Id, saved.ClientId);
            Assert.AreEqual(_indexId, saved.IndexClientId);

            var savedSister = _memberScreeningService.OpenEncounter(encounterSister.Id);
            Assert.NotNull(savedSister);
            Assert.AreEqual(sister.Id, savedSister.ClientId);
            Assert.AreEqual(_indexId, savedSister.IndexClientId);
        
            var memberscreening = saved.ObsMemberScreenings.FirstOrDefault();
            Assert.NotNull(memberscreening);
            Assert.AreEqual(encounter.Id, memberscreening.EncounterId);
            Assert.AreEqual(new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"), memberscreening.Eligibility);
            Console.WriteLine(saved);

            var memberscreeningSister = savedSister.ObsMemberScreenings.FirstOrDefault();
            Assert.NotNull(memberscreeningSister);
            Assert.AreEqual(encounterSister.Id, memberscreeningSister.EncounterId);
            Assert.AreEqual(new Guid("b25ed04e-852f-11e7-bb31-be2e44b06b34"), memberscreeningSister.Eligibility);
            Console.WriteLine(savedSister);
        }


        [Test]
        public void should_Save_Screening_States()
        {
            var son = _clients[3];
            _registryService.SaveOrUpdate(son, false);
            _registryService.UpdateRelationShips("Son", _indexId, son.Id);
            var encounter = _memberScreeningService.StartEncounter(_memberscreening.FormId,
                _memberscreening.EncounterTypeId, son.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var screening = Builder<ObsMemberScreening>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Eligibility = new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"))
                .With(x => x.IndexClientId = _indexId).Build();

            _memberScreeningService.SaveMemberScreening(screening, son.Id, _indexId);
        
            var screenedSon = _registryService.Find(son.Id);
            Assert.NotNull(screenedSon);
            var states = screenedSon.ClientStates.ToList();
            Assert.True(states.Count == 3);
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.FamilyListed));
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.FamilyScreened));
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.FamilyEligibileYes));
            Console.WriteLine(screenedSon);
            foreach (var state in states)
            {
                Console.WriteLine($" {state}");
            }
        }
    }
}