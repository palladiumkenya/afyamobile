using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class ConceptTypeJson : ISeedJson<ConceptType>
    {
        public  List<ConceptType> Read()
        {
            string raw = @"
[
  {
    ^Name^: ^Multi^,
    ^Id^: ^Multi^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Numeric^,
    ^Id^: ^Numeric^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Single^,
    ^Id^: ^Single^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Text^,
    ^Id^: ^Text^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<ConceptType>>(raw.Replace("^","\""));
        }
    }
}
