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
   ^IsDefault^: 1,
   ^PracticeTypeId^: ^Facility^,
   ^CountyId^: 47,
   ^Id^: ^AB054358-98B9-11E7-ABC4-CEC278B6B50A^,
   ^Voided^: 0
 }
]
";
            return JsonConvert.DeserializeObject<List<Practice>>(raw.Replace("^","\""));
        }
    }
}
