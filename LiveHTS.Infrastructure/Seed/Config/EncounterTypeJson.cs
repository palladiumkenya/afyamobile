using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public  class EncounterTypeJson : ISeedJson<EncounterType>
    {
        public  List<EncounterType> Read()
        {
            string raw = @"
[
 {
   ^Name^: ^HTS Initial^,
   ^Id^: ^7e5164a6-6b99-11e7-907b-a6006ad3dba0^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<EncounterType>>(raw.Replace("^","\""));
        }
    }
}
