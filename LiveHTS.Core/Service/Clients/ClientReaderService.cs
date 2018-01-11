using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Clients
{
    public class ClientReaderService : IClientReaderService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientRelationshipRepository _clientRelationshipRepository;
        private readonly IEncounterRepository _encounterRepository;

        public ClientReaderService(IClientRepository clientRepository, IClientRelationshipRepository clientRelationshipRepository, IEncounterRepository encounterRepository)
        {
            _clientRepository = clientRepository;
            _clientRelationshipRepository = clientRelationshipRepository;
            _encounterRepository = encounterRepository;
        }

        public Client LoadClient(Guid clientId)
        {
            var client= _clientRepository.Get(clientId);

            if (null != client)
                client.Relationships = _clientRelationshipRepository.GetRelationships(clientId).ToList();

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
    }
}