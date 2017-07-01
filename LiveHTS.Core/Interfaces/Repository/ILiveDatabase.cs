using System.Collections.Generic;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Repository
{
    public interface ILiveDatabase
    {
        List<Module> Read();
        List<ConceptType> ReadConceptTypes();
        List<ConceptLookupItem> ReadConceptLookupItems();
    }
}