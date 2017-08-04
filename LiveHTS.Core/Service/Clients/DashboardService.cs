using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Clients
{
    public class DashboardService:IDashboardService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientRelationshipRepository _clientRelationshipRepository;
        private readonly IFormRepository _formRepository;

        public DashboardService(IClientRepository clientRepository, IFormRepository formRepository, IClientRelationshipRepository clientRelationshipRepository)
        {
            _clientRepository = clientRepository;
            _formRepository = formRepository;
            _clientRelationshipRepository = clientRelationshipRepository;
        }

        public Client LoadClient(Guid clientId)
        {
            var client= _clientRepository.Get(clientId);

            if (null != client)
                client.Relationships = _clientRelationshipRepository.GetRelationships(clientId).ToList();

            return client;
        }

        public IEnumerable<Form> LoadForms(Guid? moduleId = null)
        {
            if (!moduleId.IsNullOrEmpty())
                return _formRepository.GetAll(x => x.ModuleId == moduleId).ToList();

            return _formRepository.GetAll().ToList();
        }
    }
}