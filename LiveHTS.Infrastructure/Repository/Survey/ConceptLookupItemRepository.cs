using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.DummyData;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ConceptLookupItemRepository : BaseRepository<ConceptLookupItem>, IConceptLookupItemRepository
    {
        public ConceptLookupItemRepository()
        {
            _entities = LiveDatabase.ReadConceptLookupItems().ToList();
        }
    }
}