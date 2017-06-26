using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ConceptTypeRepository : BaseRepository<ConceptType>, IConceptTypeRepository
    {
        public ConceptTypeRepository()
        {
            _entities = new List<ConceptType>
            {
                new ConceptType {Name = "Text", Description = "Text"},
                new ConceptType {Name = "Numeric", Description = "Numeric"},
                new ConceptType {Name = "Coded", Description = "Coded"}
            };
        }
    }
}