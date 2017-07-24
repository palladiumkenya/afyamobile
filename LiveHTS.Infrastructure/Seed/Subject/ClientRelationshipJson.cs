using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Subject
{
  public  class ClientRelationshipJson : ISeedJson<ClientRelationship>
    {
        public  List<ClientRelationship> Read()
        {
            string raw = @"
[
 {
   ^RelationshipTypeId^: ^Partner^,
   ^RelatedClientId^: ^4547b7e0-98c7-4c6f-9d2a-a7b7016df232^,
   ^Preferred^: 1,
   ^ClientId^: ^4547b7e0-98c7-4c6f-9d2a-a7b7016df234^,
   ^Id^: ^7e51629e-6b99-11e7-907b-a6006ad4dba0^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<ClientRelationship>>(raw.Replace("^","\""));
        }
    }
}
