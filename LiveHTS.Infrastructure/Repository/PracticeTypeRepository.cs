using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model;

namespace LiveHTS.Infrastructure.Repository
{
    public class PracticeTypeRepository:BaseRepository<PracticeType>, IPracticeTypeRepository
    {         
        public PracticeTypeRepository()
        {
            _entities = new List<PracticeType>{
                new PracticeType("Facility", "Health Facility"),
                new PracticeType("Surveillance", "Surveillance")
            };
        }      
    }
}