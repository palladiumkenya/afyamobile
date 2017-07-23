using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed
{
  public  class UserJson
    {
        public static List<User> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^62040ce6-6260-11e7-907b-a6006ad3dba0^,
    ^Name^: ^HTS User^,
    ^Display^: ^Hiv Testing Services User^,
    ^Description^: ^Hiv Testing Services User^,
    ^Rank^: 1,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<User>>(raw.Replace("^","\""));
        }
    }
}
