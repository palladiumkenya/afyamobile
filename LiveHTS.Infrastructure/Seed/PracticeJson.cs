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
    ^Id^: ^62040ce6-6260-11e7-907b-a6006ad3dba0^,
    ^Name^: ^HTS Practice^,
    ^Display^: ^Hiv Testing Services Practice^,
    ^Description^: ^Hiv Testing Services Practice^,
    ^Rank^: 1,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Practice>>(raw.Replace("^","\""));
        }
    }
}
