using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class RelationshipTypeJson : ISeedJson<RelationshipType>
    {
        public  List<RelationshipType> Read()
        {
            string raw = @"
[
  {
    ^Name^: ^Partner^,
    ^Description^: ^Partner^,
    ^Id^: ^Partner^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Brother^,
    ^Description^: ^Family^,
    ^Id^: ^Brother^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Cowife^,
    ^Description^: ^Family^,
    ^Id^: ^Cowife^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Daugther^,
    ^Description^: ^Family^,
    ^Id^: ^Daugther^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Father^,
    ^Description^: ^Family^,
    ^Id^: ^Father^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Mother^,
    ^Description^: ^Family^,
    ^Id^: ^Mother^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Sister^,
    ^Description^: ^Family^,
    ^Id^: ^Sister^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Son^,
    ^Description^: ^Family^,
    ^Id^: ^Son^,
    ^Voided^: 0
  },
  {
    ^Name^: ^Spouse^,
    ^Description^: ^Family^,
    ^Id^: ^Spouse^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<RelationshipType>>(raw.Replace("^","\""));
        }
    }
}
