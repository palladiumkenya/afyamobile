using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class ConceptJson : ISeedJson<Concept>
    {
        public  List<Concept> Read()
        {
            string raw = @"
[
  {
    ^Name^: ^Consent^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^62040a3e-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^00c2a60a-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Result^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^62040b24-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^00c2aa06-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Name^: ^No of Kits^,
    ^ConceptTypeId^: ^Numeric^,
    ^CategoryId^: ^^,
    ^Id^: ^00c2b14a-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Referall^,
    ^ConceptTypeId^: ^Multi^,
    ^CategoryId^: ^62040c00-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^00c2b23a-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Remarks^,
    ^ConceptTypeId^: ^Text^,
    ^CategoryId^: ^^,
    ^Id^: ^00c2b550-6246-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Discordant^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^62040a3e-6260-11e7-907b-a6006ad3dba0^,
    ^Id^: ^6203cad8-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Concept>>(raw.Replace("^","\""));
        }
    }
}
