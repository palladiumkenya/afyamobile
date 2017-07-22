using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Service.Clients
{
    public class RegistryService:IRegistryService
    {
        private readonly IClientRepository _clientRepository;

        public RegistryService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAllClients(string search = "")
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Client client)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid clientId)
        {
            throw new NotImplementedException();
        }
    }
}