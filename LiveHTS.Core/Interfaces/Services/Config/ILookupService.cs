using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;

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
        IEnumerable<MaritalStatus> GetMaritalStatuses(bool addSelectOption=false, string selectOption = "[Select Option]");
        IEnumerable<KeyPop> GetKeyPops(bool addSelectOption = false, string selectOption = "[Select Option]");
        IEnumerable<IdentifierType> GetIdentifierTypes();
        IEnumerable<RelationshipType> GetRelationshipTypes();
        EncounterType GetDefaultEncounterType(Guid? id=null);

        IEnumerable<CategoryItem> GetCategoryItems(string code,bool addSelectOption=false,string selectOption="[Select Option]");
    }
}