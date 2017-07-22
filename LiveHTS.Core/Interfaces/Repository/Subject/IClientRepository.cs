﻿using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Repository.Subject
{
    public interface IClientRepository:IRepository<Client,Guid>
    {
        void SaveOrUpdate(Client client);
        IEnumerable<Client> QuickSearch(string search);
    }
}