﻿using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Repository.Survey
{
    public interface IConceptRepository:IRepository<Concept, Guid>
    {
        IEnumerable<Concept> GetWithLookups();
    }
}