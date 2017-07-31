using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Config
{
  public class PracticeJson : ISeedJson<Practice>
    {
        public  List<Practice> Read()
        {
            string raw = @"
[
 {
   ^Code^: 13023,
   ^Name^: ^Kenyatta National Hospital^,
   ^PracticeTypeId^: ^Facility^,
   ^CountyId^: 47,
   ^Id^: ^7e51629e-6b99-11e7-907b-a6006ad3dba0^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<Practice>>(raw.Replace("^","\""));
        }
    }
}
