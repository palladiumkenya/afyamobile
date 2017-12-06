﻿using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Service.Clients
{
    public class RemoteSearchService : IRemoteSearchService
    {
        public RemoteSearchService()
        {
        
        }
        public IEnumerable<Client> GetAllClients(string search = "")
        {
          return new List<Client>();
        }
    }
}