﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.SmartCard;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Service.Clients
{
    public class RegistryService:IRegistryService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientIdentifierRepository _clientIdentifierRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IClientRelationshipRepository _clientRelationshipRepository;
        private readonly IEncounterRepository _encounterRepository;
        private readonly IClientStateRepository _clientStateRepository;
        private readonly IPSmartStoreRepository _pSmartStoreRepository;

        public RegistryService(IClientRepository clientRepository, IClientIdentifierRepository clientIdentifierRepository, IPersonRepository personRepository, IClientRelationshipRepository clientRelationshipRepository, IEncounterRepository encounterRepository, IClientStateRepository clientStateRepository, IPSmartStoreRepository pSmartStoreRepository)
        {
            _clientRepository = clientRepository;
            _clientIdentifierRepository = clientIdentifierRepository;
            _personRepository = personRepository;
            _clientRelationshipRepository = clientRelationshipRepository;
            _encounterRepository = encounterRepository;
            _clientStateRepository = clientStateRepository;
            _pSmartStoreRepository = pSmartStoreRepository;
        }
        public Client Load(Guid id)
        {
            return _clientRepository.Load(id);
        }
        public Client Find(Guid id)
        {
            return _clientRepository.Get(id);
        }

        public IEnumerable<Client> GetAllClients(string search = "")
        {
            if (string.IsNullOrWhiteSpace(search))
                return _clientRepository.GetAll();


            var clientIds = new List<Guid>();
            var personIds = new List<Guid>();

            var cIds = _clientIdentifierRepository.GetAll(x => x.Identifier.ToLower().Contains(search.ToLower()))
                .Select(x => x.ClientId)
                .ToList();
            var pIds = _personRepository.GetAll(
                    x => x.FirstName.ToLower().Contains(search.ToLower()) ||
                         x.MiddleName.ToLower().Contains(search.ToLower()) ||
                         x.LastName.ToLower().Contains(search.ToLower())
                )
                .Select(x => x.Id)
                .ToList();

            clientIds.AddRange(cIds);
            personIds.AddRange(pIds);

            var clients= new List<Client>();

            if (clientIds.Count > 0)
            {
                var cidMatch = _clientRepository.GetAll(x => clientIds.Contains(x.Id)).ToList();
                clients.AddRange(cidMatch);
            }

            if (personIds.Count > 0)
            {
                var pMatch = _clientRepository.GetAll(x => personIds.Contains(x.PersonId)).ToList();
                clients.AddRange(pMatch);
            }

            return clients;
        }

        public IEnumerable<Client> GetAllSiteClients(Guid siteId, string search = "")
        {
            return GetAllClients(search).Where(x => x.PracticeId == siteId);
        }

        public IEnumerable<Client> GetAllCohortClients(Guid cohortId, string search = "")
        {
            if (string.IsNullOrWhiteSpace(search))
                return _clientRepository.GetAll(x=>x.CohortId==cohortId);


            var clientIds = new List<Guid>();
            var personIds = new List<Guid>();

            var cIds = _clientIdentifierRepository.GetAll(x => x.Identifier.ToLower().Contains(search.ToLower()))
                .Select(x => x.ClientId)
                .ToList();
            var pIds = _personRepository.GetAll(
                    x => x.FirstName.ToLower().Contains(search.ToLower()) ||
                         x.MiddleName.ToLower().Contains(search.ToLower()) ||
                         x.LastName.ToLower().Contains(search.ToLower())
                )
                .Select(x => x.Id)
                .ToList();

            clientIds.AddRange(cIds);
            personIds.AddRange(pIds);

            var clients = new List<Client>();

            if (clientIds.Count > 0)
            {
                var cidMatch = _clientRepository.GetAll(x => x.CohortId == cohortId&&clientIds.Contains(x.Id)).ToList();
                clients.AddRange(cidMatch);
            }

            if (personIds.Count > 0)
            {
                var pMatch = _clientRepository.GetAll(x => x.CohortId == cohortId && personIds.Contains(x.PersonId)).ToList();
                clients.AddRange(pMatch);
            }

            return clients;
        }

        public void Save(Client client)
        {
            //check id in use

            if (!client.Identifiers.Any())
                throw new ArgumentException($"Client should have an Identifier !");

            var clientIdentifier = client.Identifiers.First();

            var clientIdentifiers = _clientIdentifierRepository.GetAll(
                x => x.Identifier.ToLower() == clientIdentifier.Identifier.ToLower() &&
                     x.IdentifierTypeId == clientIdentifier.IdentifierTypeId
            );
            if (clientIdentifiers.Any())
                throw new ArgumentException($"Identifier {clientIdentifier.Identifier} is already in Use !");

            //create Person
            _personRepository.Save(client.Person);

            //create Client
            _clientRepository.Save(client);
        }

        public void Save(Client client, Guid cohortId)
        {
            client.CohortId=cohortId;
            Save(client);
        }

        public void UpdateRelationShips(string relationshipTypeId, Guid clientId, Guid otherClientId)
        {
            
            var exisitngRelationship = _clientRelationshipRepository.Find(relationshipTypeId, clientId, otherClientId);

            if (null == exisitngRelationship)
            {
                var newRelation = ClientRelationship.Create(relationshipTypeId, otherClientId, true, clientId,false);
                _clientRelationshipRepository.Save(newRelation);
                var state = RelationshipType.IsPartner(relationshipTypeId)
                    ? LiveState.HtsPatlisted
                    : LiveState.HtsFamlisted;
                _clientStateRepository.SaveOrUpdate(new ClientState(clientId,state));
            }

            var exisitngRelationshipReverse = _clientRelationshipRepository.Find(relationshipTypeId, otherClientId, clientId);
            if (null == exisitngRelationshipReverse)
            {
                //otherClientId  clientId
                var newRelationReverse = ClientRelationship.Create(relationshipTypeId, clientId, true, otherClientId, true);
                _clientRelationshipRepository.Save(newRelationReverse);
                var state = RelationshipType.IsPartner(relationshipTypeId)
                    ? LiveState.PartnerListed
                    : LiveState.FamilyListed;
                _clientStateRepository.SaveOrUpdate(new ClientState(otherClientId, state, clientId));
            }
        }



     
        public void SaveOrUpdate(Client client,bool isClient= true)
        {
            //check id in use

            if (isClient)
            {

                if (!client.Identifiers.Any())
                    throw new ArgumentException($"Client should have an Identifier !");

                var clientIdentifier = client.Identifiers.First();

                var clientIdentifiers = _clientIdentifierRepository.GetAll(
                    x => x.Identifier.ToLower() == clientIdentifier.Identifier.ToLower() &&
                         x.IdentifierTypeId == clientIdentifier.IdentifierTypeId &&
                         x.ClientId != client.Id

                );

                if (clientIdentifiers.Any())
                    throw new ArgumentException($"Identifier {clientIdentifier.Identifier} is already in Use !");
            }

            //create Person
            _personRepository.InsertOrUpdate(client.Person);

            //create Client
            _clientRepository.InsertOrUpdate(client);

            if (isClient)
            {
                _clientStateRepository.SaveOrUpdate(new ClientState(client.Id, LiveState.HtsEnrolled));
                _clientStateRepository.SaveOrUpdate(new ClientState(client.Id, LiveState.HtsFamAcceptedYes));
            }
        }

        public Guid SaveOrGet(Client client, bool isClient = true, bool isTested = true)
        {
            //check id in use

            if (isClient)
            {

                if (!client.Identifiers.Any())
                    throw new ArgumentException($"Client should have an Identifier !");

                var clientIdentifier = client.Identifiers.First();

                var clientIdentifiers = _clientIdentifierRepository.GetAll(
                    x => x.Identifier.ToLower() == clientIdentifier.Identifier.ToLower() &&
                         x.IdentifierTypeId == clientIdentifier.IdentifierTypeId &&
                         x.ClientId != client.Id

                );

                if (clientIdentifiers.Any())
                {
                    return clientIdentifiers.First().ClientId;
                }
            }

            //create Person
            _personRepository.InsertOrUpdate(client.Person);

            //create Client
            _clientRepository.InsertOrUpdate(client);

            if (isClient)
            {
                _clientStateRepository.SaveOrUpdate(new ClientState(client.Id, LiveState.HtsEnrolled));
                _clientStateRepository.SaveOrUpdate(new ClientState(client.Id, LiveState.HtsFamAcceptedYes));
                _clientStateRepository.SaveOrUpdate(new ClientState(client.Id, LiveState.HtsSmartCardEnrolled));
                if(isTested)
                    _clientStateRepository.SaveOrUpdate(new ClientState(client.Id, LiveState.HtsTested));
            }

            return client.Id;
        }

        public void SaveDownloaded(Client client)
        {
            //create Person
            _personRepository.InsertOrUpdate(client.Person);

            //create Client
            _clientRepository.SaveDownloaded(client);
        }

        public void SaveOrUpdateContact(Client client)
        {
            throw new NotImplementedException();
        }

        public async Task Download(Client client,List<Encounter> encounters)
        {
            await Task.Run(() =>
            {
                client.Downloaded = true;
                SaveDownloaded(client);
                foreach (var encounter in encounters)
                {
                    _encounterRepository.Upload(encounter);
                }
            });
        }

        public async Task<Guid> SaveShr(Client shrClient, bool isTested = true)
        {
            Guid clientId;

            await Task.Run(() =>
            {
              clientId=SaveOrGet(shrClient);
            });

            return clientId;
        }

        public void UpdateSmartCardEnrolled(Guid clientId)
        {
            _clientStateRepository.SaveOrUpdate(new ClientState(clientId, LiveState.HtsSmartCardEnrolled));
        }

        public void UpdateSmartCardShr(Guid clientId, string shr)
        {
            var storeSummary = PSmartStore.Create(shr, clientId);
            _pSmartStoreRepository.SaveOrUpdate(storeSummary);
        }

        public void Delete(Guid clientId)
        {
            _clientRepository.Delete(clientId);
        }
    }
}