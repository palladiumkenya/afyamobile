using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.SmartCard;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Service.Clients
{
    public class ClientReaderService : IClientReaderService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientRelationshipRepository _clientRelationshipRepository;
        private readonly IEncounterRepository _encounterRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IPSmartStoreRepository _pSmartStoreRepository;
        private readonly IClientStateRepository _clientStateRepository;

        public ClientReaderService(IClientRepository clientRepository, IClientRelationshipRepository clientRelationshipRepository, IEncounterRepository encounterRepository, IPersonRepository personRepository, IPSmartStoreRepository pSmartStoreRepository, IClientStateRepository clientStateRepository)
        {
            _clientRepository = clientRepository;
            _clientRelationshipRepository = clientRelationshipRepository;
            _encounterRepository = encounterRepository;
            _personRepository = personRepository;
            _pSmartStoreRepository = pSmartStoreRepository;
            _clientStateRepository = clientStateRepository;
        }

        public Client LoadClient(Guid clientId)
        {
            var client= _clientRepository.Get(clientId);

            if (null != client)
            {
                client.Relationships = _clientRelationshipRepository.GetRelationships(clientId).ToList();
                client.IsPretestComplete = _encounterRepository.CheckPretestComplete(clientId,client.Downloaded);
            }

            return client;
        }

        public List<Guid> LoadClientIds()
        {
            return _clientRepository.GetAllClientIds().ToList();
        }

        public List<Encounter> LoadEncounters(Guid clientId)
        {
            return _encounterRepository.LoadAll(clientId).ToList();
        }

        public List<PSmartStore> LoadPSmartStores(Guid clientId)
        {
            return _pSmartStoreRepository.LoadAll(clientId).ToList();
        }

        public bool CheckPretestComplete(Guid clientId)
        {
            return _encounterRepository.CheckPretestComplete(clientId);
        }

        public void Purge(ClientToDeleteDTO toDeleteDto)
        {
            foreach (var enconterToDeleteDto in toDeleteDto.EnconterToDeleteDtos)
            {
                _encounterRepository.Purge(enconterToDeleteDto.Id,enconterToDeleteDto.Name);
                _encounterRepository.Delete(enconterToDeleteDto.Id);
            }

            _clientRepository.Purge(toDeleteDto.Id);

            _personRepository.Delete(toDeleteDto.PersonId);

            _clientRelationshipRepository.Purge(toDeleteDto.Id);

            Purge(toDeleteDto.Id);


        }

        public void Purge(Guid id)
        {
            var rel = _clientRelationshipRepository.GetAll(x => x.ClientId == id).Select(x => x.RelatedClientId)
                .ToList();

            foreach (var guid in rel)
            {
              _encounterRepository.PurgeAny(guid);

                _clientRepository.Delete(id);

                _personRepository.Purge(id);

                _clientRelationshipRepository.PurgeRel(guid);

                Purge(guid);
            }
        }
    }
}
