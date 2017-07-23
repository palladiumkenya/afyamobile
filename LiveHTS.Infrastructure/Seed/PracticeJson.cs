using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed
{
  public  class PracticeJson
    {
        public static List<Practice> Read()
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
