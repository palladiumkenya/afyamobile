using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Service.Clients
{
    public class RegistryService:IRegistryService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientIdentifierRepository _clientIdentifierRepository;
        private readonly IPersonRepository _personRepository;

        public RegistryService(IClientRepository clientRepository, IClientIdentifierRepository clientIdentifierRepository, IPersonRepository personRepository)
        {
            _clientRepository = clientRepository;
            _clientIdentifierRepository = clientIdentifierRepository;
            _personRepository = personRepository;
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

            return _clientRepository
                .GetAll(x => clientIds.Contains(x.Id) ||
                             personIds.Contains(x.PersonId))
                .ToList();

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