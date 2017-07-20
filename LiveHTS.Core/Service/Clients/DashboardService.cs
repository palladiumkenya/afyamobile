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
        private readonly IFormRepository _formRepository;

        public DashboardService(IClientRepository clientRepository, IFormRepository formRepository)
        {
            _clientRepository = clientRepository;
            _formRepository = formRepository;
        }

        public Client LoadClient(Guid clientId)
        {
            return _clientRepository.Get(clientId);
        }

        public IEnumerable<Form> LoadForms(Guid? moduleId = null)
        {
            if (!moduleId.IsNullOrEmpty())
                return _formRepository.GetAll(x => x.ModuleId == moduleId).ToList();

            return _formRepository.GetAll().ToList();
        }
    }
}