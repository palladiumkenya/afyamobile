using System.Collections.Generic;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Config
{
    public interface ILookupService
    {
        IEnumerable<County> GetCounties();
        IEnumerable<SubCounty> GetSubCounties(int[] countyIds);
        IEnumerable<PracticeType> GetPracticeTypes();
        Practice GetDefault();
        IEnumerable<Practice> GetDefaultPractices();
        IEnumerable<Practice> GetPractices(string[] typeIds);
        IEnumerable<MaritalStatus> GetMaritalStatuses();
        IEnumerable<KeyPop> GetKeyPops();
        IEnumerable<IdentifierType> GetIdentifierTypes();
        IEnumerable<RelationshipType> GetRelationshipTypes();
    }
}