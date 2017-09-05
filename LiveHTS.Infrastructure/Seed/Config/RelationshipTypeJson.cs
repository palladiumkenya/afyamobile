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
   ^Id^: ^Partner^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<RelationshipType>>(raw.Replace("^","\""));
        }
    }
}
