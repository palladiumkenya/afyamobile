using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;


namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ConceptRepository : BaseRepository<Concept, Guid>, IConceptRepository
    {
        public ConceptRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
       
        public IEnumerable<Concept> GetWithLookups()
        {
            throw new NotImplementedException();
        }
    }
}