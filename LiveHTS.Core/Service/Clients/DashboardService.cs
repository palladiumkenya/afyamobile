using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Clients
{
    public class DashboardService:IDashboardService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientRelationshipRepository _clientRelationshipRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IEncounterRepository _encounterRepository;

        public DashboardService(IClientRepository clientRepository, IClientRelationshipRepository clientRelationshipRepository, IModuleRepository moduleRepository, IEncounterRepository encounterRepository)
        {
            _clientRepository = clientRepository;
            _clientRelationshipRepository = clientRelationshipRepository;
            _moduleRepository = moduleRepository;
            _encounterRepository = encounterRepository;
        }

        public Client LoadClient(Guid clientId)
        {
            var client= _clientRepository.Get(clientId);

            if (null != client)
                client.Relationships = _clientRelationshipRepository.GetRelationships(clientId).ToList();

            return client;
        }
        public Module LoadModule()
        {
            return _moduleRepository.GetDefaultModule();
        }

        public List<Module> LoadModules()
        {
            return _moduleRepository.GetAll().ToList();
        }

        public void RemoveRelationShip(Guid id)
        {
            _clientRelationshipRepository.Delete(id);
        }

        public void RemoveEncounter(Guid id)
        {
            _encounterRepository.Delete(id);
        }
    }
}