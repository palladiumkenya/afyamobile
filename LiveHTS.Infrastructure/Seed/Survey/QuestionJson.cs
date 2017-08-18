using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class QuestionJson : ISeedJson<Question>
    {
        public  List<Question> Read()
        {
            string raw = @"
[
  {
    ^ConceptId^: ^00c2a60a-6246-11e7-907b-a6006ad3dba0^,
    ^Ordinal^: 1,
    ^Display^: ^Consent^,
    ^Rank^: 1,
    ^FormId^: ^62040dcc-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206a9a6-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConceptId^: ^00c2a60a-6246-11e7-907b-a6006ad3dba0^,
    ^Ordinal^: ^1.a^,
    ^Display^: ^Smoke ?^,
    ^Rank^: 1.1,
    ^FormId^: ^62040dcc-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206a9a6-6260-11e8-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConceptId^: ^00c2a60a-6246-11e7-907b-a6006ad3dba0^,
    ^Ordinal^: ^1.b^,
    ^Display^: ^Drink ?^,
    ^Rank^: 1.2,
    ^FormId^: ^62040dcc-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206a9a6-6260-11e9-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConceptId^: ^00c2aa06-6246-11e7-907b-a6006ad3dba0^,
    ^Ordinal^: 2,
    ^Display^: ^Result^,
    ^Rank^: 2,
    ^FormId^: ^62040dcc-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206aa78-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConceptId^: ^00c2b14a-6246-11e7-907b-a6006ad3dba0^,
    ^Ordinal^: 3,
    ^Display^: ^No of Kits^,
    ^Rank^: 3,
    ^FormId^: ^62040dcc-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206ab4a-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConceptId^: ^00c2b23a-6246-11e7-907b-a6006ad3dba0^,
    ^Ordinal^: 4,
    ^Display^: ^Referall^,
    ^Rank^: 4,
    ^FormId^: ^62040dcc-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206ac1c-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConceptId^: ^6203cad8-6260-11e7-907b-a6006ad3dba0^,
    ^Ordinal^: 5,
    ^Display^: ^Discordant^,
    ^Rank^: 5,
    ^FormId^: ^62040dcc-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206acf8-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^ConceptId^: ^00c2b550-6246-11e7-907b-a6006ad3dba0^,
    ^Ordinal^: 6,
    ^Display^: ^Remarks^,
    ^Rank^: 6,
    ^FormId^: ^62040dcc-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6206b13a-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Question>>(raw.Replace("^","\""));
        }
    }
}
