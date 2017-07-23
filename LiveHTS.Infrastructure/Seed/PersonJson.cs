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
  public  class PersonJson
    {
        public static List<Person> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^62040ce6-6260-11e7-907b-a6006ad3dba0^,
    ^Name^: ^HTS Person^,
    ^Display^: ^Hiv Testing Services Person^,
    ^Description^: ^Hiv Testing Services Person^,
    ^Rank^: 1,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Person>>(raw.Replace("^","\""));
        }
    }
}
