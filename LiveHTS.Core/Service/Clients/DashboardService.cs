using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Service.Clients
{
    public class DashboardService:IDashboardService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientRelationshipRepository _clientRelationshipRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IEncounterRepository _encounterRepository;
        private readonly IClientStateRepository _clientStateRepository;

        public DashboardService(IClientRepository clientRepository, IClientRelationshipRepository clientRelationshipRepository, IModuleRepository moduleRepository, IEncounterRepository encounterRepository, IClientStateRepository clientStateRepository)
        {
            _clientRepository = clientRepository;
            _clientRelationshipRepository = clientRelationshipRepository;
            _moduleRepository = moduleRepository;
            _encounterRepository = encounterRepository;
            _clientStateRepository = clientStateRepository;
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

        public void RemoveRelationShipInState(Guid clientId, Guid otherClientId, bool isFamily = true)
        {
            var remainingReleations = _clientRelationshipRepository.GetRelationships(clientId).ToList();
            if (isFamily)
            {
                var famRelations = remainingReleations.Where(x => x.IsFamilyRelation()).ToList();

                if (famRelations.Count == 0)
                {
                    _clientStateRepository.DeleteState(clientId, null, LiveState.HtsFamlisted);
                }

                _clientStateRepository.DeleteState(otherClientId, null, LiveState.FamilyListed, clientId);
            }
            else
            {
                var patRelations = remainingReleations.Where(x => x.IsPartnerRelation()).ToList();

                if (patRelations.Count == 0)
                {
                    _clientStateRepository.DeleteState(clientId, null, LiveState.HtsPatlisted);
                }

                _clientStateRepository.DeleteState(otherClientId, null, LiveState.PartnerListed, clientId);
            }
        }

        public void RemoveEncounter(Guid id)
        {
            _encounterRepository.Delete(id);
            _encounterRepository.Purge(id,string.Empty);
        }
    }
}