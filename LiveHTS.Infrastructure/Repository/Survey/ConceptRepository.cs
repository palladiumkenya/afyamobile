using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.DummyData;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ConceptRepository : BaseRepository<Concept>, IConceptRepository
    {

        public ConceptRepository()
        {
            foreach (var m in LiveDatabase.Read())
            {
                foreach (var f in m.Forms)
                {
                    foreach (var s in f.Sections)
                    {
                        _entities.AddRange(s.Concepts);
                    }
                }
            }
        }
    }
}