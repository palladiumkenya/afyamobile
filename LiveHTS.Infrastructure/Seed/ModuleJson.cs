using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed
{
  public  class ModuleJson
    {
        public static List<Module> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^62040ce6-6260-11e7-907b-a6006ad3dba0^,
    ^Name^: ^HTS Module^,
    ^Display^: ^Hiv Testing Services Module^,
    ^Description^: ^Hiv Testing Services Module^,
    ^Rank^: 1,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Module>>(raw.Replace("^","\""));
        }
    }
}
