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
            return _clientRepository.Get(id);
        }

        public IEnumerable<Client> GetAllClients(string search = "")
        {
            if (string.IsNullOrWhiteSpace(search))
                return _clientRepository.GetAll();

            return _clientRepository.GetAll();
        }

        public void SaveOrUpdate(Client client)
        {
            
            _clientRepository.SaveOrUpdate(client);
        }

        public void Delete(Guid clientId)
        {
            _clientRepository.Delete(clientId);
        }
    }
}