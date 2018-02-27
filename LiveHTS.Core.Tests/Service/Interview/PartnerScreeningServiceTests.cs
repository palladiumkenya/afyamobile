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
    public class PartnerScreeningServiceTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database;

        private IRegistryService _registryService;
        private IPartnerScreeningService _partnerScreeningService;
        private List<Client> _clients;
        private Client _client;
        private Guid _indexId = new Guid("f2243aff-9f91-4dd0-85fc-a89401258075");

        private IClientRepository _clientRepository;
        private IClientIdentifierRepository _clientIdentifierRepository;
        private IPersonRepository _personRepository;
        private IClientRelationshipRepository _clientRelationshipRepository;
        private IEncounterRepository _encounterRepository;
        private ClientStateRepository _clientStateRepository;

        private IObsPartnerScreeningRepository _partnerScreeningRepository;
        private ICategoryRepository _categoryRepository;
    
        private Program _partnerscreening = new Program(new Guid("b25ec112-852f-11e7-bb31-be2e46b06b37"),
            new Guid("b262fda4-877f-11e7-bb31-be2e44b68b34"));

     

        [OneTimeSetUp]
        public void Init()
        {
            _clients = TestDataHelpers.GetTestClients(3);
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
            _partnerScreeningRepository = new ObsPartnerScreeningRepository(_liveSetting);
            _registryService = new RegistryService(_clientRepository, _clientIdentifierRepository, _personRepository,
                _clientRelationshipRepository, _encounterRepository, _clientStateRepository);

            _partnerScreeningService = new PartnerScreeningService(_encounterRepository, _partnerScreeningRepository,
                _categoryRepository, _clientStateRepository);
        }

        [Test]
        public void should_Save_Screening()
        {
            var partner = _clients.Last();
            _registryService.SaveOrUpdate(partner, false);
            _registryService.UpdateRelationShips("Partner", _indexId, partner.Id);

            var encounter = _partnerScreeningService.StartEncounter(_partnerscreening.FormId,
                _partnerscreening.EncounterTypeId, partner.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var screening = Builder<ObsPartnerScreening>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Eligibility = new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"))
                .With(x => x.IndexClientId = _indexId).Build();

            _partnerScreeningService.SavePartnerScreening(screening, partner.Id, _indexId);

            var saved = _partnerScreeningService.OpenEncounter(encounter.Id);
            Assert.NotNull(saved);
            Assert.AreEqual(partner.Id, saved.ClientId);
            Assert.AreEqual(_indexId, saved.IndexClientId);
            var partnerscreening = saved.ObsPartnerScreenings.FirstOrDefault();
            Assert.NotNull(partnerscreening);
            Assert.AreEqual(encounter.Id, partnerscreening.EncounterId);
            Assert.AreEqual(new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"), partnerscreening.Eligibility);
            Console.WriteLine(saved);
       
        }
        [Test]
        public void should_Save_Screening_Multiple()
        {
            var partner = _clients[0];
            var cowife = _clients[1];

            _registryService.SaveOrUpdate(partner, false);
            _registryService.SaveOrUpdate(cowife, false);

            _registryService.UpdateRelationShips("Partner", _indexId, partner.Id);
            _registryService.UpdateRelationShips("Cowife", _indexId, cowife.Id);

            var encounter = _partnerScreeningService.StartEncounter(_partnerscreening.FormId,
                _partnerscreening.EncounterTypeId, partner.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);

            var encounterCowife = _partnerScreeningService.StartEncounter(_partnerscreening.FormId,
                _partnerscreening.EncounterTypeId, cowife.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);

            var screening = Builder<ObsPartnerScreening>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Eligibility = new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"))
                .With(x => x.IndexClientId = _indexId).Build();

            var screeningCowife = Builder<ObsPartnerScreening>.CreateNew()
                .With(x => x.EncounterId = encounterCowife.Id)
                .With(x => x.Eligibility = new Guid("b25ed04e-852f-11e7-bb31-be2e44b06b34"))
                .With(x => x.IndexClientId = _indexId).Build();

            _partnerScreeningService.SavePartnerScreening(screening, partner.Id, _indexId);
            _partnerScreeningService.SavePartnerScreening(screeningCowife, cowife.Id, _indexId);
            
            var saved = _partnerScreeningService.OpenEncounter(encounter.Id);
            Assert.NotNull(saved);
            Assert.AreEqual(partner.Id, saved.ClientId);
            Assert.AreEqual(_indexId, saved.IndexClientId);

            var savedCowife = _partnerScreeningService.OpenEncounter(encounterCowife.Id);
            Assert.NotNull(savedCowife);
            Assert.AreEqual(cowife.Id, savedCowife.ClientId);
            Assert.AreEqual(_indexId, savedCowife.IndexClientId);
        
            var partnerscreening = saved.ObsPartnerScreenings.FirstOrDefault();
            Assert.NotNull(partnerscreening);
            Assert.AreEqual(encounter.Id, partnerscreening.EncounterId);
            Assert.AreEqual(new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"), partnerscreening.Eligibility);
            Console.WriteLine(saved);

            var partnerscreeningCowife = savedCowife.ObsPartnerScreenings.FirstOrDefault();
            Assert.NotNull(partnerscreeningCowife);
            Assert.AreEqual(encounterCowife.Id, partnerscreeningCowife.EncounterId);
            Assert.AreEqual(new Guid("b25ed04e-852f-11e7-bb31-be2e44b06b34"), partnerscreeningCowife.Eligibility);
            Console.WriteLine(savedCowife);
        }


        [Test]
        public void should_Save_Screening_States()
        {
            var partner = _clients.Last();
            _registryService.SaveOrUpdate(partner, false);
            _registryService.UpdateRelationShips("Partner", _indexId, partner.Id);
            var encounter = _partnerScreeningService.StartEncounter(_partnerscreening.FormId,
                _partnerscreening.EncounterTypeId, partner.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), _indexId);
            var screening = Builder<ObsPartnerScreening>.CreateNew()
                .With(x => x.EncounterId = encounter.Id)
                .With(x => x.Eligibility = new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"))
                .With(x => x.IndexClientId = _indexId).Build();

            _partnerScreeningService.SavePartnerScreening(screening, partner.Id, _indexId);
        
            var screenedPartner = _registryService.Find(partner.Id);
            Assert.NotNull(screenedPartner);
            var states = screenedPartner.ClientStates.ToList();
            Assert.True(states.Count == 3);
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.PartnerListed));
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.PartnerScreened));
            Assert.NotNull(states.FirstOrDefault(x => x.Status == LiveState.PartnerEligibileYes));
            Console.WriteLine(screenedPartner);
            foreach (var state in states)
            {
                Console.WriteLine($" {state}");
            }
        }
    }
}